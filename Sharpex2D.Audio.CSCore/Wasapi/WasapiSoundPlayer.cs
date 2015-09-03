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
using CSCore.SoundOut;
using CSCore.Streams;
using Sharpex2D.Framework.Audio;
using PlaybackState = Sharpex2D.Framework.Audio.PlaybackState;

namespace Sharpex2D.Audio.Wasapi
{
    internal class WasapiSoundPlayer : ISoundPlayer
    {
        private readonly ISoundOut _soundOut;
        private PanSource _panSource;
        private PlaybackMode _playbackMode;
        private bool _userStopped;
        private VolumeSource _volumeSource;

        /// <summary>
        /// Initializes a new WasapiSoundPlayer class.
        /// </summary>
        public WasapiSoundPlayer()
        {
            _soundOut = new WasapiOut();
            _soundOut.Stopped += SoundOutStopped;
            PlaybackDevice = new WasapiDevice(((WasapiOut)_soundOut).Device);
        }

        /// <summary>
        /// Sets or gets the Pan.
        /// </summary>
        public float Pan
        {
            get { return _panSource?.Pan ?? 0; }
            set
            {
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
            get { return _volumeSource?.Volume ?? 0; }
            set
            {
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
            get { return _soundOut.WaveSource?.GetPosition().Milliseconds ?? 0; }
            set
            {
                if (_soundOut.WaveSource != null)
                {
                    Seek(value);
                }
            }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get
            {
                if (_soundOut == null)
                {
                    return PlaybackState.Stopped;
                }

                switch (_soundOut.PlaybackState)
                {
                    case CSCore.SoundOut.PlaybackState.Paused:
                        return PlaybackState.Paused;
                    case CSCore.SoundOut.PlaybackState.Playing:
                        return PlaybackState.Playing;
                    case CSCore.SoundOut.PlaybackState.Stopped:
                        return PlaybackState.Stopped;
                }

                return PlaybackState.Stopped;
            }
        }


        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length => _soundOut.WaveSource?.GetLength().Milliseconds ?? 0;
        
        /// <summary>
        /// Gets or sets the playback device
        /// </summary>
        public IPlaybackDevice PlaybackDevice { set; get; }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        /// <summary>
        /// Initializes the sound player with the given source.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="format">The Format.</param>
        public void Initialize(Stream stream, Framework.Audio.WaveFormat format)
        {
            var reader = new RawDataReader(stream,
                new CSCore.WaveFormat(format.SamplesPerSec, format.BitsPerSample,
                    format.Channels));

            _volumeSource = new VolumeSource(reader);
            _panSource = new PanSource(_volumeSource);

            if (PlaybackDevice == null)
                throw new NullReferenceException("PlaybackDevice was null.");

            ((WasapiOut) _soundOut).Device = ((WasapiDevice) PlaybackDevice).MMDevice;
            _soundOut.Initialize(_panSource.ToWaveSource());
            _soundOut.Volume = 1f;
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="playbackMode">The PlaybackMode.</param>
        public void Play(PlaybackMode playbackMode)
        {
            _userStopped = false;
            _playbackMode = playbackMode;
            _soundOut.Play();
            RaisePlaybackChanged();
        }


        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _soundOut.Resume();
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            _soundOut.Pause();
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Stops the sound.
        /// </summary>
        public void Stop()
        {
            _userStopped = true;
            _soundOut.Stop();
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            _soundOut.WaveSource?.SetPosition(new TimeSpan(0, 0, 0, 0, (int) position));
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
        /// Triggered if the sound out stopped.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void SoundOutStopped(object sender, PlaybackStoppedEventArgs e)
        {
            if (!_userStopped && _playbackMode == PlaybackMode.Loop)
            {
                Play(PlaybackMode.Loop);
            }
            else
            {
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Deconstructs the WasapiAudioProvider class.
        /// </summary>
        ~WasapiSoundPlayer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _soundOut.Stop();
                _soundOut.Dispose();
                _volumeSource.Dispose();
                _panSource.Dispose();
            }
        }

        /// <summary>
        /// Raises the PlaybackChanged Event.
        /// </summary>
        private void RaisePlaybackChanged()
        {
            PlaybackChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
