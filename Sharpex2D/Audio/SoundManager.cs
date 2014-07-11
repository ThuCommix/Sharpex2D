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

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class SoundManager : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CFC1385E-27C0-4EC1-9EF9-5580C8F1CDE9"); }
        }

        #endregion

        private readonly ISoundInitializer _soundInitializer;
        private readonly ISoundProvider _soundProvider;
        private bool _muted;
        private float _vBeforeMute;

        /// <summary>
        ///     Initializes a new SoundManager class.
        /// </summary>
        /// <param name="soundInitializer">The ISoundInitializer.</param>
        public SoundManager(ISoundInitializer soundInitializer)
        {
            _soundProvider = soundInitializer.CreateProvider();
            _soundInitializer = soundInitializer;
        }

        /// <summary>
        ///     Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _soundProvider.Balance; }
            set { _soundProvider.Balance = value; }
        }

        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _soundProvider.Volume; }
            set { _soundProvider.Volume = value; }
        }

        /// <summary>
        ///     Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _soundProvider.Position; }
            set { _soundProvider.Position = value; }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return _soundProvider.IsPlaying; }
        }

        /// <summary>
        ///     Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _soundProvider.Length; }
        }

        /// <summary>
        ///     A value indicating whether the sound is muted.
        /// </summary>
        public bool Muted
        {
            set
            {
                if ((value && Muted) | (!value && !Muted))
                {
                    return;
                }

                if (value)
                {
                    _vBeforeMute = Volume;
                    Volume = 0;
                }
                else
                {
                    Volume = _vBeforeMute;
                }

                _muted = value;
            }
            get { return _muted; }
        }

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        /// <param name="sound">The Soundfile.</param>
        public void Play(Sound sound)
        {
            if (!sound.IsInitialized)
            {
                throw new ArgumentException("The sound is not initialized.");
            }

            _soundProvider.Play(sound, PlayMode.None);
        }

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        /// <param name="sound">The Soundfile.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound sound, PlayMode playMode)
        {
            if (!sound.IsInitialized)
            {
                throw new ArgumentException("The sound is not initialized.");
            }

            _soundProvider.Play(sound, playMode);
        }

        /// <summary>
        ///     Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _soundProvider.Resume();
        }

        /// <summary>
        ///     Pause a sound.
        /// </summary>
        public void Pause()
        {
            _soundProvider.Pause();
        }

        /// <summary>
        ///     Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            _soundProvider.Seek(position);
        }

        /// <summary>
        ///     Creates a new SoundManager.
        /// </summary>
        /// <returns>SoundManager.</returns>
        internal SoundManager CreateNew()
        {
            return new SoundManager(_soundInitializer);
        }
    }
}