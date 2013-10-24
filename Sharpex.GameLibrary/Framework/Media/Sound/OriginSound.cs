using System;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Media.Sound.OriginTypes;

namespace SharpexGL.Framework.Media.Sound
{
    public class OriginSound
    {
        /// <summary>
        /// Initializes a new OriginSound class.
        /// </summary>
        /// <param name="originSoundType">The OriginType.</param>
        /// <param name="sound">The Sound.</param>
        public OriginSound(Sound sound, IOriginType originSoundType)
        {
            _soundManager = (SoundManager) SGL.Components.Get<SoundManager>().Clone();
            OriginSoundType = originSoundType;
            Sound = sound;
        }

        /// <summary>
        /// Sets or gets the OriginSoundPosition.
        /// </summary>
        public Vector2 OriginSoundPosition { set; get; }

        /// <summary>
        /// Sets or gets the ListenerPosition.
        /// </summary>
        public Vector2 ListenerPosition { set; get; }

        /// <summary>
        /// Gets the OriginSoundType.
        /// </summary>
        public IOriginType OriginSoundType { get; private set; }

        /// <summary>
        /// Gets the Sound.
        /// </summary>
        public Sound Sound { private set; get; }

        private readonly SoundManager _soundManager;

        /// <summary>
        /// Updates the OriginSound Component.
        /// </summary>
        /// <remarks>You should only update after a position change.</remarks>
        public void Update()
        {
            Update(ListenerPosition, OriginSoundPosition);
        }

        /// <summary>
        /// Updates the OriginSound Component.
        /// </summary>
        /// <param name="listenerPosition">The ListenerPosition.</param>
        /// <param name="soundPosition">The SoundPosition.</param>
        /// <remarks>This could be abstract later.</remarks>
        /// <remarks>You should only update after a position change.</remarks>
        public void Update(Vector2 listenerPosition, Vector2 soundPosition)
        {
            var circleOriginType = OriginSoundType as CircleOriginType;
            if (circleOriginType != null)
            {
                CircleProcessing(circleOriginType);
            }

            throw new InvalidOperationException("IOriginType (" + OriginSoundType.GetType().Name + "{" +
                                                OriginSoundType.Guid + "}) could not resolved.");
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="sound">The Soundfile.</param>
        public void Play(Sound sound)
        {
            if (_soundManager != null)
            {
                _soundManager.Play(sound, PlayMode.None);
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
            if (_soundManager != null)
            {
                _soundManager.Play(sound, playMode);
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
            if (_soundManager != null)
            {
                _soundManager.Resume();
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
            if (_soundManager != null)
            {
                _soundManager.Pause();
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
            if (_soundManager != null)
            {
                _soundManager.Seek(position);
            }
            else
            {
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
                if (_soundManager != null)
                {
                    return _soundManager.Position;
                }
                throw new SoundProviderNotInitializedException();
            }
            set
            {
                if (_soundManager != null)
                {
                    _soundManager.Position = value;
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
                if (_soundManager != null)
                {
                    return _soundManager.IsPlaying;
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
                if (_soundManager != null)
                {
                    return _soundManager.Length;
                }
                throw new SoundProviderNotInitializedException();
            }
        }

        #region Internal

        /// <summary>
        /// Processes the sound for a circle origin.
        /// </summary>
        /// <param name="type">The CircleOriginType.</param>
        private void CircleProcessing(CircleOriginType type)
        {
            var originDistance = (OriginSoundPosition - ListenerPosition).Length;
            if (originDistance > type.Radius)
            {
                //listener is out of range.
                _soundManager.Volume = 0;
            }
            else
            {
                var volume = originDistance/type.Radius; //8 / 10 = 0.8
                _soundManager.Volume = 1f - volume; //1 - 0.8 = 0.2 volume
                if (ListenerPosition.X > OriginSoundPosition.X)
                {
                    //balance right
                    _soundManager.Balance = 0.75f;
                }
                else if (ListenerPosition.X < OriginSoundPosition.X)
                {
                    //balance left
                    _soundManager.Balance = 0.25f;
                }
                else
                {
                    //balance mid
                    _soundManager.Balance = 0.5f;
                }
            }
        }

        #endregion
    }
}
