using System;

namespace Sharpex2D.Framework.Media.Sound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class SoundEffect
    {
        private readonly SoundManager _soundProvider;
        private bool _muted;
        private Sound _sound;
        private float _vBeforeMute;

        /// <summary>
        ///     Initializes a new SoundEffect.
        /// </summary>
        public SoundEffect()
        {
            _soundProvider = (SoundManager) SGL.Components.Get<SoundManager>().Clone();
            Volume = 0.5f;
            _vBeforeMute = 0.5f;
        }

        /// <summary>
        ///     Initializes a new SoundEffect.
        /// </summary>
        /// <param name="sound">The Sound.</param>
        public SoundEffect(Sound sound)
        {
            _soundProvider = (SoundManager) SGL.Components.Get<SoundManager>().Clone();
            _sound = sound;
            Volume = 0.5f;
            _vBeforeMute = 0.5f;
        }

        /// <summary>
        ///     Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            set { _soundProvider.Balance = value; }
            get { return _soundProvider.Balance; }
        }

        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            set { _soundProvider.Volume = value; }
            get { return _soundProvider.Volume; }
        }

        /// <summary>
        ///     Sets or gets the Sound.
        /// </summary>
        public Sound Sound
        {
            set { _sound = value; }
            get { return _sound; }
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
        ///     Plays the SoundEffect.
        /// </summary>
        public void Play()
        {
            if (_sound != null)
            {
                _soundProvider.Play(_sound, PlayMode.None);
            }
            else
            {
                throw new NullReferenceException("Sound can not be null.");
            }
        }
    }
}