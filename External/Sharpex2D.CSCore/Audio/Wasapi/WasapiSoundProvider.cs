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
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams;

namespace Sharpex2D.Audio.Wasapi
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class WasapiSoundProvider : ISoundProvider
    {
        #region IComponent Implementation

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0910E001-3926-413A-9AA9-E33E08D497D1"); }
        }

        #endregion

        private bool _disposed;
        private PanSource _panSource;
        private WasapiOut _wasapiOut;

        /// <summary>
        /// Initializes a new WasapiSoundProvider class.
        /// </summary>
        /// <param name="soundInitializer">The SoundInitializer.</param>
        internal WasapiSoundProvider(ISoundInitializer soundInitializer)
        {
            _wasapiOut = new WasapiOut(false, AudioClientShareMode.Shared, 100);
            SoundInitializer = soundInitializer;
            _wasapiOut.Stopped += DirectSoundOutStopped;
        }

        /// <summary>
        /// Gets the SoundInitializer.
        /// </summary>
        public ISoundInitializer SoundInitializer { private set; get; }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get
            {
                switch (_wasapiOut.PlaybackState)
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
        /// Plays the sound.
        /// </summary>
        /// <param name="soundFile">The Soundfile.</param>
        public void Play(Sound soundFile)
        {
            Play(CodecFactory.Instance.GetCodec(soundFile.ResourcePath));
            if (PlaybackChanged != null)
                PlaybackChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _wasapiOut.Resume();
            if (PlaybackChanged != null)
                PlaybackChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            _wasapiOut.Pause();
            if (PlaybackChanged != null)
                PlaybackChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_wasapiOut.WaveSource != null)
                _wasapiOut.WaveSource.Position = position;
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _wasapiOut.WaveSource != null ? _wasapiOut.WaveSource.Position : 0; }
            set { Seek(value); }
        }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _wasapiOut.WaveSource != null ? _wasapiOut.WaveSource.Length : 0; }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event PlaybackChangedEventHandler PlaybackChanged;

        /// <summary>
        /// Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _panSource.Pan; }
            set { _panSource.Pan = value; }
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _wasapiOut.Volume; }
            set { _wasapiOut.Volume = value; }
        }

        /// <summary>
        /// Disposes the SoundProvider.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Stops the sound.
        /// </summary>
        public void Stop()
        {
            if (PlaybackState != PlaybackState.Stopped)
            {
                _wasapiOut.Stop();
            }
        }

        /// <summary>
        /// Triggerd if the DSoundOut stopped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirectSoundOutStopped(object sender, EventArgs e)
        {
            if (PlaybackChanged != null)
                PlaybackChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="source">The WaveSource.</param>
        private void Play(IWaveSource source)
        {
            Stop();
            var panSource = new PanSource(source);
            _panSource = panSource;
            _wasapiOut.Initialize(panSource.ToWaveSource());
            _wasapiOut.Play();
        }

        /// <summary>
        /// Disposes the SoundProvider.
        /// </summary>
        /// <param name="disposing">The State.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_wasapiOut != null)
                {
                    Stop();
                    _wasapiOut.Dispose();
                    _wasapiOut = null;
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Deconstructs the SoundProvider.
        /// </summary>
        ~WasapiSoundProvider()
        {
            Dispose(false);
        }
    }
}