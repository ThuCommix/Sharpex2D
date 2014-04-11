using Sharpex2D.Framework.Media.Sound;
using Sharpex2D.Framework.Media.Video;

namespace Sharpex2D.Framework.Media
{
    public class MediaInitializer
    {
        /// <summary>
        /// Initializes a new MediaInitializer class.
        /// </summary>
        public MediaInitializer()
        {
            SoundInitializer = null;
            VideoInitializer = null;
        }
        /// <summary>
        /// Initializes a new MediaInitializer class.
        /// </summary>
        /// <param name="soundInitializer">The SoundInitializer.</param>
        public MediaInitializer(ISoundInitializer soundInitializer)
        {
            SoundInitializer = soundInitializer;
            VideoInitializer = null;
        }
        /// <summary>
        /// Initializes a new MediaInitializer class.
        /// </summary>
        /// <param name="videoInitializer">The VideoInitializer.</param>
        public MediaInitializer(IVideoInitializer videoInitializer)
        {
            SoundInitializer = null;
            VideoInitializer = videoInitializer;
        }
        /// <summary>
        /// Initializes a new MediaInitializer class.
        /// </summary>
        /// <param name="soundInitializer">The SoundInitializer.</param>
        /// <param name="videoInitializer">The VideoInitializer.</param>
        public MediaInitializer(ISoundInitializer soundInitializer, IVideoInitializer videoInitializer)
        {
            SoundInitializer = soundInitializer;
            VideoInitializer = videoInitializer;
        }
        /// <summary>
        /// Gets the SoundInitializer.
        /// </summary>
        public ISoundInitializer SoundInitializer { private set; get; }
        /// <summary>
        /// Gets the VideoInitializer.
        /// </summary>
        public IVideoInitializer VideoInitializer { private set; get; }
    }
}
