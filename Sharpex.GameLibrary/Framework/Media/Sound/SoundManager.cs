using System;
using SharpexGL.Framework.Collections;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Media.Sound
{
    public class SoundManager : IComponent,  ICloneable
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CFC1385E-27C0-4EC1-9EF9-5580C8F1CDE9"); }
        }

        #endregion

        private readonly ISoundProvider _soundProvider;
        private bool _muted;
        private float _vBeforeMute;

        public SoundManager(ISoundInitializer soundInitializer)
        {
            if (soundInitializer == null) return;
            _soundProvider = soundInitializer.CreateProvider();
            _vBeforeMute = 0.5f;
            Volume = 0.5f;
            SoundEffects = new BufferedCollection<SoundEffect>();
        }

        /// <summary>
        /// Sets or gets the SoundEffects.
        /// </summary>
        public BufferedCollection<SoundEffect> SoundEffects { set; get; } 

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="sound">The Soundfile.</param>
        public void Play(Sound sound)
        {
            if (_soundProvider != null)
            {
                _soundProvider.Play(sound, PlayMode.None);
            }
            else
            {
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="sound">The Soundfile.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound sound, PlayMode playMode)
        {
            if (_soundProvider != null)
            {
                _soundProvider.Play(sound, playMode);
            }
            else
            {
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            if (_soundProvider != null)
            {
                _soundProvider.Resume();
            }
            else
            {
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            if (_soundProvider != null)
            {
                _soundProvider.Pause();
            }
            else
            {
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_soundProvider != null)
            {
                _soundProvider.Seek(position);
            }
            else
            {
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get
            {
                if (_soundProvider != null)
                {
                    return _soundProvider.Balance;
                }
                throw new SoundProviderNotInitializedException();
            }
            set
            {
                if (_soundProvider != null)
                {
                    _soundProvider.Balance = value;
                    return;
                }
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get
            {
                if (_soundProvider != null)
                {
                    return _soundProvider.Volume;
                }
                throw new SoundProviderNotInitializedException();
            }
            set
            {
                if (_soundProvider != null)
                {
                    _soundProvider.Volume = value;
                    return;
                }
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get
            {
                if (_soundProvider != null)
                {
                    return _soundProvider.Position;
                }
                throw new SoundProviderNotInitializedException();
            }
            set
            {
                if (_soundProvider != null)
                {
                    _soundProvider.Position = value;
                    return;
                }
                throw new SoundProviderNotInitializedException();
            }           
        }
        /// <summary>
        /// A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                if (_soundProvider != null)
                {
                    return _soundProvider.IsPlaying;
                }
                throw new SoundProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get
            {
                if (_soundProvider != null)
                {
                    return _soundProvider.Length;
                }
                throw new SoundProviderNotInitializedException();
            }
        }

        /// <summary>
        /// A value indicating whether the sound is muted.
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

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
