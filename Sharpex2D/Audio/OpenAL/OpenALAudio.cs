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
using System.Threading.Tasks;

namespace Sharpex2D.Framework.Audio.OpenAL
{
    internal class OpenALAudio : IDisposable
    {
        private readonly SoundMixer _audioMixer;
        private readonly object _locker;
        private readonly OpenALSource _source;
        private bool _beginDispose;
        private int _bufferSize;
        private Task _playbackThread;
        private int _processedBytes;
        private Stream _stream;

        /// <summary>
        /// Initializes a new OpenALAudioBuffer class.
        /// </summary>
        public OpenALAudio(OpenALAudioFormat format, OpenALDevice device)
        {
            Format = format;
            Owner = device;
            _locker = new object();
            _audioMixer = new SoundMixer();
            PlaybackState = PlaybackState.Stopped;
            _source = Owner.SourcePool.RequestSource();
        }

        /// <summary>
        /// Gets the SampleRate.
        /// </summary>
        public uint SampleRate { get; private set; }

        /// <summary>
        /// Gets the AudioFormat.
        /// </summary>
        public OpenALAudioFormat Format { get; }

        /// <summary>
        /// Gets the WaveFormat.
        /// </summary>
        public WaveFormat WaveFormat { get; private set; }

        /// <summary>
        /// Gets the owner of this buffer.
        /// </summary>
        public OpenALDevice Owner { get; }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _audioMixer.Volume; }
            set { _audioMixer.Volume = value; }
        }

        /// <summary>
        /// Gets or sets the Pan.
        /// </summary>
        public float Pan
        {
            get { return _audioMixer.Pan; }
            set { _audioMixer.Pan = value; }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState { get; private set; }

        /// <summary>
        /// Gets the Latency.
        /// </summary>
        public int Latency { private set; get; }

        /// <summary>
        /// Gets the Position of the sound.
        /// </summary>
        public long Position { private set; get; }

        /// <summary>
        /// Gets the Length of the sound.
        /// </summary>
        public long Length { private set; get; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        /// <summary>
        /// Seeks the source to the given position.
        /// </summary>
        /// <param name="position">The Position in ms.</param>
        public void Seek(long position)
        {
            _processedBytes = (int) position/1000*WaveFormat.AvgBytesPerSec;
        }

        /// <summary>
        /// Deconstructs the OpenALAudioBuffer class.
        /// </summary>
        ~OpenALAudio()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes the OpenALAudioBuffer class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="format">The WaveFormat.</param>
        public void Initialize(Stream stream, WaveFormat format)
        {
            Initialize(stream, format, 150);
        }

        /// <summary>
        /// Initializes the OpenALAudioBuffer class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="format">The WaveFormat.</param>
        /// <param name="latency">The Latency.</param>
        public void Initialize(Stream stream, WaveFormat format, int latency)
        {
            lock (_locker)
            {
                Owner.Context.MakeCurrent();

                OpenALInterops.alListener3f(SourceProperty.Position, 0, 0, 1);
                OpenALInterops.alListener3f(SourceProperty.Velocity, 0, 0, 0);
                OpenALInterops.alListenerfv(SourceProperty.Orientation, new float[] {0, 0, 1, 0, 1, 0});
                OpenALInterops.alSourcef(_source.SourceId, SourceProperty.Gain, _audioMixer.Volume);
                OpenALInterops.alSourcefv(_source.SourceId, SourceProperty.Position, new[] {_audioMixer.Pan, 0, 0});
            }

            SampleRate = (uint) format.SamplesPerSec;
            Latency = latency;
            _stream = stream;
            WaveFormat = format;
            Length = stream.Length/format.AvgBytesPerSec*1000;
            _bufferSize = format.AvgBytesPerSec/1000*latency;
            _processedBytes = 0;
        }

        /// <summary>
        /// Starts the playback.
        /// </summary>
        public void Play()
        {
            if (PlaybackState == PlaybackState.Stopped)
            {
                StartPlayback();
            }
        }

        /// <summary>
        /// Stops the playback.
        /// </summary>
        public void Stop()
        {
            lock (_locker)
            {
                Owner.Context.MakeCurrent();
                OpenALInterops.alSourceStop(_source.SourceId);
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
                Owner.Context.MakeCurrent();
                OpenALInterops.alSourcePause(_source.SourceId);
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
                Owner.Context.MakeCurrent();
                OpenALInterops.alSourcePlay(_source.SourceId);
                PlaybackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Starts the playback.
        /// </summary>
        private void StartPlayback()
        {
            if (PlaybackState == PlaybackState.Stopped)
            {
                PlaybackState = PlaybackState.Playing;
                RaisePlaybackChanged();

                _processedBytes = 0;

                _playbackThread = new Task(() =>
                {
                    OpenALDataBuffer buffer1;
                    OpenALDataBuffer buffer2;
                    OpenALDataBuffer buffer3;

                    lock (_locker)
                    {
                        Owner.Context.MakeCurrent();
                        buffer1 = OpenALDataBuffer.CreateBuffer();
                        buffer2 = OpenALDataBuffer.CreateBuffer();
                        buffer3 = OpenALDataBuffer.CreateBuffer();
                    }

                    buffer1.Next = buffer2;
                    buffer2.Next = buffer3;
                    buffer3.Next = buffer1;

                    OpenALDataBuffer currentBuffer = buffer1;
                    FillBuffer(currentBuffer);
                    currentBuffer = currentBuffer.Next;
                    FillBuffer(currentBuffer);
                    currentBuffer = currentBuffer.Next;
                    FillBuffer(currentBuffer);

                    lock (_locker)
                    {
                        OpenALInterops.alSourcePlay(_source.SourceId);
                    }

                    while (_processedBytes < _stream.Length)
                    {
                        if (_beginDispose) return;
                        switch (PlaybackState)
                        {
                            case PlaybackState.Paused:
                                _playbackThread.Wait(Latency);
                                continue;
                            case PlaybackState.Stopped:
                                return;
                        }

                        int finishedBuffers;
                        lock (_locker)
                        {
                            OpenALInterops.alGetSourcei(_source.SourceId, SourceProperty.AllBuffersProcessed,
                                out finishedBuffers);
                        }

                        if (finishedBuffers == 0)
                        {
                            _playbackThread.Wait(Latency);
                        }

                        while (finishedBuffers > 0)
                        {
                            finishedBuffers--;
                            currentBuffer = currentBuffer.Next;
                            FillBuffer(currentBuffer);
                        }

                        Position = _processedBytes/WaveFormat.AvgBytesPerSec*1000;

                        //if the audio stops where it should not, restart playback
                        lock (_locker)
                        {
                            int sourceState;
                            OpenALInterops.alGetSourcei(_source.SourceId, SourceProperty.SourceState, out sourceState);
                            if ((SourceState) sourceState == SourceState.Stopped)
                            {
                                OpenALInterops.alSourcePlay(_source.SourceId);
                            }
                        }
                    }

                    PlaybackState = PlaybackState.Stopped;
                    var queuedBuffers = new uint[3];
                    RaisePlaybackChanged();

                    lock (_locker)
                    {
                        OpenALInterops.alSourceUnqueueBuffers(_source.SourceId, 3, queuedBuffers);
                        OpenALInterops.alDeleteBuffers(3, queuedBuffers);
                    }
                });
                _playbackThread.Start();
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected virtual void Dispose(bool disposing)
        {
            _beginDispose = true;
            if (PlaybackState != PlaybackState.Stopped)
            {
                lock (_locker)
                {
                    OpenALInterops.alSourceStop(_source.SourceId);
                    PlaybackState = PlaybackState.Stopped;
                    RaisePlaybackChanged();
                }
            }

            if (_source.SourceId != 0)
            {
                lock (_locker)
                {
                    var queuedBuffers = new uint[3];
                    OpenALInterops.alSourceUnqueueBuffers(_source.SourceId, 3, queuedBuffers);
                    OpenALInterops.alDeleteBuffers(3, queuedBuffers);
                }
                Owner.SourcePool.FreeSource(_source);
                Owner.DestroyAudioBuffer(this);
            }

            if (disposing)
            {
            }
        }

        /// <summary>
        /// Raises the PlaybackChanged event.
        /// </summary>
        private void RaisePlaybackChanged()
        {
            if (PlaybackChanged != null)
            {
                PlaybackChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Fills the buffer.
        /// </summary>
        /// <param name="target">The OpenALDataBuffer.</param>
        private void FillBuffer(OpenALDataBuffer target)
        {
            lock (_locker)
            {
                var unqueueBuffer = new uint[1];
                OpenALInterops.alSourceUnqueueBuffers(_source.SourceId, 1, unqueueBuffer);
                var data = new byte[_bufferSize];
                int datalength = _bufferSize;
                if (_bufferSize > _stream.Length - _processedBytes)
                {
                    _processedBytes += _stream.Read(data, 0, (int) _stream.Length - _processedBytes);
                }
                else
                {
                    _processedBytes += _stream.Read(data, 0, _bufferSize);
                }
                _audioMixer.ApplyEffects(data, WaveFormat);
                OpenALInterops.alBufferData(target.Id, Format, data, datalength, SampleRate);
                OpenALInterops.alSourceQueueBuffers(_source.SourceId, 1, new[] {target.Id});
            }
        }
    }
}
