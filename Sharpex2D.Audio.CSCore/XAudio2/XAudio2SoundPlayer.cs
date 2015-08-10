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
using CSCore;
using CSCore.Codecs.RAW;
using CSCore.Codecs.WAV;
using CSCore.Streams;
using CSCore.XAudio2;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Audio;

namespace Sharpex2D.Audio.XAudio2
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [MetaData("Title", "XAudio2")]
    internal class XAudio2SoundPlayer : ISoundPlayer
    {
        private readonly XAudio2_8 _xAudio;
        private EventWaveSource _currentWaveSource;
        private float _pan;
        private PanSource _panSource;
        private PlaybackMode _playbackMode;
        private long _position;
        private StreamingSourceVoice _sourceVoice;
        private bool _userStopped;
        private float _volume;
        private VolumeSource _volumeSource;

        /// <summary>
        /// Initializes a new XAudio2SoundPlayer class.
        /// </summary>
        public XAudio2SoundPlayer()
        {
            _xAudio = new XAudio2_8();
            _xAudio.CreateMasteringVoice();
        }

        /// <summary>
        /// Sets or gets the Pan.
        /// </summary>
        public float Pan
        {
            get { return _panSource == null ? _pan : _panSource.Pan; }
            set
            {
                _pan = value;
                if (_panSource != null)
                {
                    _panSource.Pan = value;
                }
            }
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _volumeSource == null ? _volume : _volumeSource.Volume; }
            set
            {
                _volume = value;
                if (_volumeSource != null)
                {
                    _volumeSource.Volume = value;
                }
            }
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _currentWaveSource == null ? 0 : _currentWaveSource.GetPosition().Milliseconds; }
            set
            {
                _position = value;
                if (_currentWaveSource != null)
                {
                    Seek(value);
                }
            }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState { get; private set; }


        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _currentWaveSource == null ? 0 : _currentWaveSource.GetLength().Milliseconds; }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        /// <summary>
        /// Initializes the sound player with the given source.
        /// </summary>
        /// <param name="audioData">The AudioData.</param>
        /// <param name="format">The Format.</param>
        public void Initialize(byte[] audioData, Framework.Audio.WaveFormat format)
        {
            var memoryStream = new MemoryStream(audioData);
            var reader = new RawDataReader(memoryStream,
                new CSCore.WaveFormat(format.SamplesPerSec, format.BitsPerSample,
                    format.Channels));

            _volumeSource = new VolumeSource(reader);
            _panSource = new PanSource(_volumeSource);
            _currentWaveSource = new EventWaveSource(_panSource.ToWaveSource());
            _currentWaveSource.EndOfStream += XAudio2EndOfStream;
            _panSource.Pan = _pan;
            _volumeSource.Volume = _volume;
            Seek(_position);
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="playbackMode">The PlaybackMode.</param>
        public void Play(PlaybackMode playbackMode)
        {
            _userStopped = false;
            _playbackMode = playbackMode;
            StreamingSourceVoiceListener.Default.Add(_sourceVoice);
            _sourceVoice = StreamingSourceVoice.Create(_xAudio, _currentWaveSource);
            _sourceVoice.Start();
            PlaybackState = PlaybackState.Playing;
            RaisePlaybackChanged();
        }


        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _sourceVoice.Start();
            PlaybackState = PlaybackState.Playing;
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            _sourceVoice.Stop(SourceVoiceStopFlags.None, CSCore.XAudio2.XAudio2.CommitAll);
            PlaybackState = PlaybackState.Paused;
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Stops the sound.
        /// </summary>
        public void Stop()
        {
            _sourceVoice.Stop(SourceVoiceStopFlags.None, CSCore.XAudio2.XAudio2.CommitAll);
            PlaybackState = PlaybackState.Stopped;
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_currentWaveSource == null)
                return;

            _currentWaveSource.SetPosition(new TimeSpan(0, 0, 0, 0, (int) position));
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the XAudio2AudioProvider class.
        /// </summary>
        ~XAudio2SoundPlayer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Triggered if the current WaveSource is end of stream.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void XAudio2EndOfStream(object sender, EventArgs e)
        {
            if (!_userStopped && _playbackMode == PlaybackMode.Loop)
            {
                Play(PlaybackMode.Loop);
            }
            else
            {
                PlaybackState = PlaybackState.Stopped;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_sourceVoice != null)
                {
                    StreamingSourceVoiceListener.Default.Remove(_sourceVoice);
                    _sourceVoice.Stop(SourceVoiceStopFlags.None, CSCore.XAudio2.XAudio2.CommitAll);
                }
            }
        }

        /// <summary>
        /// Raises the PlaybackChanged Event.
        /// </summary>
        private void RaisePlaybackChanged()
        {
            if (PlaybackChanged != null)
            {
                PlaybackChanged(this, EventArgs.Empty);
            }
        }
    }
}