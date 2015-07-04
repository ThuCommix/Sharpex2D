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

namespace Sharpex2D.Framework.Audio.OpenAL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.InProgress)]
    [MetaData("Title", "OpenAL")]
    internal class OpenALSoundPlayer : ISoundPlayer
    {
        private OpenALAudio _audio;
        private byte[] _sounddata;
        private float _pan;
        private PlaybackMode _playbackMode;
        private bool _userStop;
        private float _volume = 0.5f;

        /// <summary>
        /// Initializes a new OpenALSoundPlayer class.
        /// </summary>
        public OpenALSoundPlayer()
        {
            if (OpenALDevice.AvailableDevices.Length == 0)
                throw new SoundException("No available audio devices where found.");
            OpenALDevice.SetDefaultOpenALDeviceOnStartup(OpenALDevice.AvailableDevices[0]);
        }

        /// <summary>
        /// Sets or gets the Pan.
        /// </summary>
        public float Pan
        {
            get { return _audio != null ? _audio.Pan : 0; }
            set
            {
                if (_audio != null)
                {
                    _audio.Pan = value;
                }

                _pan = value;
            }
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _audio != null ? _audio.Volume : 0; }
            set
            {
                if (_audio != null)
                {
                    _audio.Volume = value;
                }

                _volume = value;
            }
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _audio != null ? _audio.Position : 0; }
            set { Seek(value); }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _audio != null ? _audio.PlaybackState : PlaybackState.Stopped; }
        }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _audio != null ? _audio.Length : 0; }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        /// <summary>
        /// Initializes the sound player with the given sound source.
        /// </summary>
        /// <param name="stream">The WaveStream.</param>
        public void Initialize(WaveStream stream)
        {
            //clean old playback if existent
            if (_audio != null)
            {
                _audio.Dispose();
            }

            OpenALAudioFormat audioFormat = DetectAudioFormat(stream.Format);
            _audio = OpenALDevice.DefaultDevice.CreateAudioBuffer(audioFormat);
            _audio.PlaybackChanged += AudioPlaybackChanged;
            Volume = _volume;
            Pan = _pan;

            stream.Seek(0, SeekOrigin.Begin);
            _sounddata = new byte[stream.Length];
            stream.Read(_sounddata, 0, _sounddata.Length);

            _audio.Initialize(_sounddata, stream.Format);
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="playbackMode">The PlaybackMode.</param>
        public void Play(PlaybackMode playbackMode)
        {
            _playbackMode = playbackMode;
            _userStop = false;
            _audio.Play();
        }

        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            if (_audio != null)
            {
                _audio.Resume();
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            if (_audio != null)
            {
                _audio.Pause();
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Stops the sound.
        /// </summary>
        public void Stop()
        {
            if (_audio != null)
            {
                _audio.Stop();
                _userStop = true;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_audio != null)
            {
                _audio.Seek(position);
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            OpenALDevice.DefaultDevice.Dispose();
            _sounddata = null;
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
        /// Detects the OpenALAudioformat.
        /// </summary>
        /// <param name="format">The WaveFormat.</param>
        /// <returns>OpenALAudioFormat.</returns>
        private OpenALAudioFormat DetectAudioFormat(WaveFormat format)
        {
            if (format.Channels > 1)
            {
                return format.BitsPerSample == 8
                    ? OpenALAudioFormat.Stereo8Bit
                    : OpenALAudioFormat.Stereo16Bit;
            }

            return format.BitsPerSample == 8
                ? OpenALAudioFormat.Mono8Bit
                : OpenALAudioFormat.Mono16Bit;
        }

        /// <summary>
        /// Called if the AudioBuffer changed its playback.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void AudioPlaybackChanged(object sender, EventArgs e)
        {
            if (PlaybackState == PlaybackState.Stopped && !_userStop && _playbackMode == PlaybackMode.Loop &&
                _sounddata != null)
            {
                Play(PlaybackMode.Loop);
            }

            RaisePlaybackChanged();
        }
    }
}