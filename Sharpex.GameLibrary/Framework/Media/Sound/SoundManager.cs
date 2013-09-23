using System;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Media.Sound
{
    public class SoundManager : IComponent,  ICloneable
    {
        private ISoundProvider _soundProvider;

        public SoundManager(ISoundInitializer soundInitializer)
        {
            if (soundInitializer == null) return;
            _soundProvider = soundInitializer.CreateProvider();
        }

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

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
