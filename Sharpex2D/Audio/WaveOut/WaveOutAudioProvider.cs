// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Audio.WaveOut
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [MetaData("Name", "WaveOut API")]
    public class WaveOutAudioProvider : IAudioProvider
    {
        private readonly int _device;
        private readonly WaveOut _waveOut;
        private IAudioSource _currentAudioSource;
        private PlaybackMode _playbackMode = PlaybackMode.None;
        private bool _userStop;

        /// <summary>
        /// Initializes a new WaveOutProvider class.
        /// </summary>
        public WaveOutAudioProvider()
        {
            var devices = WaveOut.GetDevices();
            if (devices.Length == 0) throw new AudioException("No available audio devices where found.");
            _device = 0;
            _waveOut = new WaveOut();
            _waveOut.PlaybackChanged += PlaybackChangedEvent;
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event PlaybackChangedEventHandler PlaybackChanged;

        /// <summary>
        /// Sets or gets the Balance.
        /// </summary>
        public float Pan
        {
            get { return _waveOut.Pan; }
            set { _waveOut.Pan = value; }
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _waveOut.Volume; }
            set { _waveOut.Volume = value; }
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _waveOut.Stream.Position/_waveOut.Format.AvgBytesPerSec*1000; }
            set { Seek(value); }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _waveOut.PlaybackState; }
        }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _waveOut.Stream.Length/_waveOut.Format.AvgBytesPerSec*1000; }
        }

        /// <summary>
        /// Initializes the audio provider with the given audio source.
        /// </summary>
        /// <param name="audioSource">The AudioSource.</param>
        public void Initialize(IAudioSource audioSource)
        {
            var waveOutAudioSource = audioSource as WaveOutAudioSource;
            if (waveOutAudioSource == null)
            {
                throw new InvalidOperationException("The specified audio source does not match the AudioProvider type.");
            }

            _currentAudioSource = audioSource;
            _waveOut.Device = _device;
            _waveOut.Initialize(waveOutAudioSource.WaveData, waveOutAudioSource.WaveFormat);
            _userStop = false;
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="playbackMode">The PlaybackMode.</param>
        public void Play(PlaybackMode playbackMode)
        {
            _playbackMode = playbackMode;
            _waveOut.Play();
        }

        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _waveOut.Resume();
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            _waveOut.Pause();
        }

        /// <summary>
        /// Stops the sound.
        /// </summary>
        public void Stop()
        {
            _userStop = true;
            _waveOut.Stop();
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            long requestesPostion = position/1000*_waveOut.Format.AvgBytesPerSec;
            _waveOut.Stream.Position = requestesPostion;
        }

        /// <summary>
        /// Creates an AudioSource.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>AudioSource.</returns>
        public IAudioSource CreateAudioSource(string path)
        {
            return new WaveOutAudioSource(new WaveStream(path));
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
        /// Deconstructs the WaveOutAudioProvider class.
        /// </summary>
        ~WaveOutAudioProvider()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _waveOut.Dispose();
            }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void PlaybackChangedEvent(object sender, EventArgs e)
        {
            if (PlaybackState == PlaybackState.Stopped && !_userStop && _playbackMode == PlaybackMode.Loop &&
                _currentAudioSource != null)
            {
                Play(PlaybackMode.Loop);
            }

            if (PlaybackChanged != null)
            {
                PlaybackChanged(this, e);
            }
        }
    }
#endif
}