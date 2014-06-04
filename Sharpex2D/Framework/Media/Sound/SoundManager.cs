using System;
using Sharpex2D.Framework.Collections;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Media.Sound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class SoundManager : IComponent, ICloneable
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

        #region ICloneable Implementation

        /// <summary>
        ///     Clones the Object.
        /// </summary>
        /// <returns>SoundManager.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        private readonly ISoundProvider _soundProvider;
        private bool _muted;
        private float _vBeforeMute;

        public SoundManager(ISoundInitializer soundInitializer)
        {
            _soundProvider = soundInitializer.CreateProvider();
            _vBeforeMute = 0.5f;
            Volume = 0.5f;
            SoundEffects = new BufferedCollection<SoundEffect>();
        }

        /// <summary>
        ///     Sets or gets the SoundEffects.
        /// </summary>
        public BufferedCollection<SoundEffect> SoundEffects { set; get; }

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
    }
}