using System;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Media.Video
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class VideoManager : IComponent, ICloneable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("19F51529-39CC-4119-B115-85E0CD2B71C7"); }
        }

        #endregion

        #region ICloneable Implementation

        /// <summary>
        ///     Clones the Object.
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
        ///     Initializes a new VideoManager class.
        /// </summary>
        /// <param name="videoInitializer">The VideoInitializer.</param>
        public VideoManager(IVideoInitializer videoInitializer)
        {
            _videoProvider = videoInitializer.CreateProvider();

            Volume = 0.5f;
            _vBeforeMute = 0.5f;
        }

        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _videoProvider.Volume; }
            set { _videoProvider.Volume = value; }
        }

        /// <summary>
        ///     Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _videoProvider.Position; }
            set { _videoProvider.Position = value; }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return _videoProvider.IsPlaying; }
        }

        /// <summary>
        ///     Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _videoProvider.Length; }
        }

        /// <summary>
        ///     A value indicating whether the video is muted.
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
        ///     Plays the video.
        /// </summary>
        /// <param name="video">The VideoFile.</param>
        public void Play(Video video)
        {
            if (!video.IsInitialized)
            {
                throw new ArgumentException("The video is not initialized.");
            }

            _videoProvider.Play(video);
        }

        /// <summary>
        ///     Pause a video.
        /// </summary>
        public void Pause()
        {
            _videoProvider.Pause();
        }

        /// <summary>
        ///     Resumes a video.
        /// </summary>
        public void Resume()
        {
            _videoProvider.Resume();
        }

        /// <summary>
        ///     Seeks a video to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            _videoProvider.Seek(position);
        }
    }
}