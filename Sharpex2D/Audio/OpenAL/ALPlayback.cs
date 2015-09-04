// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using System.Threading;

namespace Sharpex2D.Framework.Audio.OpenAL
{
    internal class ALPlayback : IDisposable
    {
        /// <summary>
        /// Gets the openal device
        /// </summary>
        public ALDevice Device { get; }

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public float Volume
        {
            get { return _soundMixer.Volume; }
            set { _soundMixer.Volume = value; }
        }

        /// <summary>
        /// Gets or sets the pan
        /// </summary>
        public float Pan
        {
            get { return _soundMixer.Pan; }
            set { _soundMixer.Pan = value; }
        }

        /// <summary>
        /// Gets the playback state
        /// </summary>
        public PlaybackState PlaybackState { private set; get; }

        /// <summary>
        /// Gets the length in ms
        /// </summary>
        public long Length { private set; get; }

        /// <summary>
        /// Gets the position in ms
        /// </summary>
        public long Position {
            get
            {
                if (_playbackStream == null)
                    return 0;

                return _playbackStream.Position / _waveFormat.AvgBytesPerSec * 1000;
            }
            set
            {
                if (_playbackStream == null)
                    return;

                var requestesPostion = value/1000*_waveFormat.AvgBytesPerSec;
                if (requestesPostion < _playbackStream.Length)
                    _playbackStream.Seek(requestesPostion, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// Gets the latency
        /// </summary>
        public int Latency { private set; get; }

        /// <summary>
        /// Raises when the playback state changed
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        private readonly ALSource _source;
        private Thread _playbackThread;
        private readonly object _locker;
        private readonly SoundMixer _soundMixer;
        private Stream _playbackStream;
        private WaveFormat _waveFormat;
        private int _bufferSize;
        private ALFormat _alFormat;
        private PlaybackMode _playbackMode;

        /// <summary>
        /// Initializes a new ALPlayback class
        /// </summary>
        /// <param name="device">The device</param>
        public ALPlayback(ALDevice device)
        {
            Device = device;
            _source = device.GenerateALSource();
            _locker = new object();
            _soundMixer = new SoundMixer();
            PlaybackState = PlaybackState.Stopped;
        }

        /// <summary>
        /// Deconstructs the ALPlayback class
        /// </summary>
        ~ALPlayback()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes the openal playback
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="format">The format</param>
        public void Initialize(Stream stream, WaveFormat format)
        {
            Initialize(stream, format, 150);
        }

        /// <summary>
        /// Initializes the openal playback
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="format">The format</param>
        /// <param name="latency">The latency</param>
        public void Initialize(Stream stream, WaveFormat format, int latency)
        {
            _playbackStream = stream;
            _waveFormat = format;
            Latency = latency;
            Length = stream.Length / format.AvgBytesPerSec * 1000;
            _bufferSize = format.AvgBytesPerSec / 1000 * latency;
            _alFormat = DetectAudioFormat(format);
        }

        /// <summary>
        /// Starts the playback.
        /// </summary>
        public void Play(PlaybackMode mode)
        {
            if (PlaybackState == PlaybackState.Stopped)
            {
                _playbackMode = mode;
                _playbackThread = new Thread(PlaybackThread) {IsBackground = true};
                _playbackThread.Start();
            }
        }

        /// <summary>
        /// Stops the playback.
        /// </summary>
        public void Stop()
        {
            lock (_locker)
            {
                Device.Context.MakeCurrent();
                ALInterops.alSourceStop(_source.Id);
                PlaybackState = PlaybackState.Stopped;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Pause the playback.
        /// </summary>
        public void Pause()
        {
            lock (_locker)
            {
                Device.Context.MakeCurrent();
                ALInterops.alSourcePause(_source.Id);
                PlaybackState = PlaybackState.Paused;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Resumes the playback.
        /// </summary>
        public void Resume()
        {
            lock (_locker)
            {
                Device.Context.MakeCurrent();
                ALInterops.alSourcePlay(_source.Id);
                PlaybackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Plays the stream
        /// </summary>
        private void PlaybackThread()
        {
            PlaybackState = PlaybackState.Playing;
            RaisePlaybackChanged();

            Device.Context.MakeCurrent();

            var buffers = CreateBuffers(4);

            FillBuffer(buffers[0]);
            FillBuffer(buffers[1]);
            FillBuffer(buffers[2]);
            FillBuffer(buffers[3]);

            ALInterops.alSourcePlay(_source.Id);

            while (_playbackStream.Position < _playbackStream.Length)
            {
                switch (PlaybackState)
                {
                    case PlaybackState.Paused:
                        Thread.Sleep(Latency);
                        continue;
                    case PlaybackState.Stopped:
                        return;
                }

                int finishedBuffersAmount;
                ALInterops.alGetSourcei(_source.Id, ALSourceParameters.AllBuffersProcessed, out finishedBuffersAmount);

                if (finishedBuffersAmount == 0)
                {
                    Thread.Sleep(Latency);
                    continue;
                }

                var finishedBuffers = new uint[finishedBuffersAmount];
                ALInterops.alSourceUnqueueBuffers(_source.Id, finishedBuffersAmount, finishedBuffers);

                foreach (var finishedBuffer in finishedBuffers)
                {
                    FillBuffer(finishedBuffer);
                }

                Position = _playbackStream.Position/_waveFormat.AvgBytesPerSec*1000;

                int sourceState;
                ALInterops.alGetSourcei(_source.Id, ALSourceParameters.SourceState, out sourceState);
                if ((ALSourceState)sourceState == ALSourceState.Stopped)
                {
                    ALInterops.alSourcePlay(_source.Id);
                }
            }

            if (_playbackMode == PlaybackMode.Loop)
            {
                _playbackThread = new Thread(PlaybackThread);
                _playbackStream.Seek(0, SeekOrigin.Begin);
                _playbackThread.Start();
            }
            else
            {
                PlaybackState = PlaybackState.Stopped;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Creates multiple openal buffers
        /// </summary>
        /// <param name="amount">The amount</param>
        /// <returns>UInt Array</returns>
        private uint[] CreateBuffers(int amount)
        {
            var bufferIds = new uint[amount];
            ALInterops.alGenBuffers(amount, bufferIds);

            return bufferIds;
        }

        /// <summary>
        /// Fills the buffer from the playback stream
        /// </summary>
        /// <param name="buffer">The buffer</param>
        private void FillBuffer(uint buffer)
        {
            var unqueueBuffer = new uint[1];
            ALInterops.alSourceUnqueueBuffers(_source.Id, 1, unqueueBuffer);

            var data = new byte[_bufferSize];

            var dataLength = _playbackStream.Length - _playbackStream.Position < _bufferSize
                ? _playbackStream.Read(data, 0, (int) (_playbackStream.Length - _playbackStream.Position))
                : _playbackStream.Read(data, 0, data.Length);

            if (dataLength == 0) return;

            _soundMixer.ApplyEffects(data, _waveFormat);

            ALInterops.alBufferData(buffer, _alFormat, data, dataLength, (uint)_waveFormat.SamplesPerSec);
            ALInterops.alSourceQueueBuffers(_source.Id, 1, new[] { buffer });
        }

        /// <summary>
        /// Detects the openal format
        /// </summary>
        /// <param name="format">The wave format</param>
        /// <returns>ALFormat</returns>
        private ALFormat DetectAudioFormat(WaveFormat format)
        {
            if (format.Channels > 1)
            {
                return format.BitsPerSample == 8
                    ? ALFormat.Stereo8Bit
                    : ALFormat.Stereo16Bit;
            }

            return format.BitsPerSample == 8
                ? ALFormat.Mono8Bit
                : ALFormat.Mono16Bit;
        }

        /// <summary>
        /// Raises the playback changed event
        /// </summary>
        private void RaisePlaybackChanged()
        {
            PlaybackChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Disposes the openal playback
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the openal playback
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected void Dispose(bool disposing)
        {
            Device.Context.MakeCurrent();

            if (disposing)
            {
                _source.Dispose();
            }

            int finishedBuffersAmount;
            ALInterops.alGetSourcei(_source.Id, ALSourceParameters.AllBuffersProcessed, out finishedBuffersAmount);

            var finishedBuffers = new uint[finishedBuffersAmount];
            ALInterops.alSourceUnqueueBuffers(_source.Id, finishedBuffersAmount, finishedBuffers);

            ALInterops.alDeleteBuffers(finishedBuffersAmount, finishedBuffers);
        }
    }
}
