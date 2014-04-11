using System;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Media.Video
{
    public class VideoManager : IComponent, ICloneable
    {
        #region IComponent Implementation
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("19F51529-39CC-4119-B115-85E0CD2B71C7"); } }
        #endregion

        #region ICloneable Implementation
        /// <summary>
        /// Clones the Object.
        /// </summary>
        /// <returns>VideoManager</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        private readonly IVideoProvider _videoProvider;
        private bool _muted;
        private float _vBeforeMute;

        /// <summary>
        /// Initializes a new VideoManager class.
        /// </summary>
        /// <param name="videoInitializer">The VideoInitializer.</param>
        public VideoManager(IVideoInitializer videoInitializer)
        {
            if (videoInitializer == null) return;
            _videoProvider = videoInitializer.CreateProvider();

            Volume = 0.5f;
            _vBeforeMute = 0.5f;
        }
        /// <summary>
        /// Plays the video.
        /// </summary>
        /// <param name="video">The VideoFile.</param>
        public void Play(Video video)
        {
            if (_videoProvider == null)
            {
                throw new VideoProviderNotInitializedException();
            }

            if (!video.IsInitialized)
            {
                throw new ArgumentException("The video is not initialized.");
            }

            _videoProvider.Play(video);
        }
        /// <summary>
        /// Pause a video.
        /// </summary>
        public void Pause()
        {
            if (_videoProvider == null)
            {
                throw new VideoProviderNotInitializedException();
            }

            _videoProvider.Pause();
        }
        /// <summary>
        /// Resumes a video.
        /// </summary>
        public void Resume()
        {
            if (_videoProvider == null)
            {
                throw new VideoProviderNotInitializedException();
            }

            _videoProvider.Resume();
        }
        /// <summary>
        /// Seeks a video to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_videoProvider == null)
            {
                throw new VideoProviderNotInitializedException();
            }

            _videoProvider.Seek(position);
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get
            {
                if (_videoProvider != null)
                {
                    return _videoProvider.Volume;
                }
                throw new VideoProviderNotInitializedException();
            }
            set
            {
                if (_videoProvider != null)
                {
                    _videoProvider.Volume = value;
                    return;
                }
                throw new VideoProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get
            {
                if (_videoProvider != null)
                {
                    return _videoProvider.Position;
                }
                throw new VideoProviderNotInitializedException();
            }
            set
            {
                if (_videoProvider != null)
                {
                    _videoProvider.Position = value;
                    return;
                }
                throw new VideoProviderNotInitializedException();
            }
        }
        /// <summary>
        /// A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                if (_videoProvider != null)
                {
                    return _videoProvider.IsPlaying;
                }
                throw new VideoProviderNotInitializedException();
            }
        }
        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get
            {
                if (_videoProvider != null)
                {
                    return _videoProvider.Length;
                }
                throw new VideoProviderNotInitializedException();
            }
        }

        /// <summary>
        /// A value indicating whether the video is muted.
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
    }
}
