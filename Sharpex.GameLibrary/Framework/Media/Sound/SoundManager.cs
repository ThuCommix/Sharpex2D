using System;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Media.Sound
{
    public class SoundManager : IComponent,  ICloneable
    {
        private readonly ISoundProvider _soundProvider;

        public SoundManager(ISoundInitializer soundInitializer)
        {
            if (soundInitializer == null) return;
            _soundProvider = soundInitializer.CreateProvider();
        }
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

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
