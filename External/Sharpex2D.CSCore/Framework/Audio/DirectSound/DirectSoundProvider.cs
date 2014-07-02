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
using CSCore.SoundOut;
using CSCore.Streams;

namespace Sharpex2D.Framework.Audio.DirectSound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DirectSoundProvider : ISoundProvider
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0910E001-3926-413A-9AA9-E33E08D497D1"); }
        }

        #endregion

        private DirectSoundOutExtended _directSoundOut;
        private bool _disposed;

        /// <summary>
        ///     Initializes a new CSCoreSoundProvider class.
        /// </summary>
        /// <param name="soundInitializer">The SoundInitializer.</param>
        internal DirectSoundProvider(ISoundInitializer soundInitializer)
        {
            _directSoundOut = new DirectSoundOutExtended();
            SoundInitializer = soundInitializer;
        }

        /// <summary>
        ///     Gets the PlaybackState.
        /// </summary>
        private PlaybackState PlaybackState
        {
            get { return _directSoundOut.PlaybackState; }
        }

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        /// <param name="soundFile">The Soundfile.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Audio.Sound soundFile, PlayMode playMode)
        {
            Play(CodecFactory.Instance.GetCodec(soundFile.ResourcePath), playMode);
        }

        /// <summary>
        ///     Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _directSoundOut.Resume();
        }

        /// <summary>
        ///     Pause a sound.
        /// </summary>
        public void Pause()
        {
            _directSoundOut.Pause();
            IsPlaying = false;
        }

        /// <summary>
        ///     Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_directSoundOut.WaveSource != null)
                _directSoundOut.WaveSource.Position = position;
        }

        /// <summary>
        ///     Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _directSoundOut.WaveSource != null ? _directSoundOut.WaveSource.Position : 0; }
            set { Seek(value); }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        ///     Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _directSoundOut.WaveSource != null ? _directSoundOut.WaveSource.Length : 0; }
        }

        /// <summary>
        ///     Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _directSoundOut.Pan; }
            set { _directSoundOut.Pan = value; }
        }

        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _directSoundOut.Volume; }
            set { _directSoundOut.Volume = value; }
        }

        /// <summary>
        ///     Gets the SoundInitializer.
        /// </summary>
        public ISoundInitializer SoundInitializer { private set; get; }

        /// <summary>
        ///     Disposes the SoundProvider.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        /// <param name="source">The WaveSource.</param>
        /// <param name="playMode">The PlayMode.</param>
        private void Play(IWaveSource source, PlayMode playMode)
        {
            Stop();
            if (playMode == PlayMode.Loop)
                source = new LoopStream(source);
            _directSoundOut.Initialize(source);
            _directSoundOut.Play();
            IsPlaying = true;
        }

        /// <summary>
        ///     Stops the sound.
        /// </summary>
        private void Stop()
        {
            if (PlaybackState != PlaybackState.Stopped)
            {
                _directSoundOut.Stop();
                IsPlaying = false;
            }
        }

        /// <summary>
        ///     Disposes the SoundProvider.
        /// </summary>
        /// <param name="disposing">The State.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_directSoundOut != null)
                {
                    Stop();
                    _directSoundOut.Dispose();
                    _directSoundOut = null;
                }
            }
            _disposed = true;
        }

        /// <summary>
        ///     Deconstructs the SoundProvider.
        /// </summary>
        ~DirectSoundProvider()
        {
            Dispose(false);
        }
    }
}