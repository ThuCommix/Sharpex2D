using System.IO;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Factory;

namespace Sharpex2D.Framework.Media.Sound
{
    public class Sound : IContent
    {
        /// <summary>
        /// Sets or Gets the resource path.
        /// </summary>
        public string ResourcePath
        {
            get;
            private set;
        }
        /// <summary>
        /// Determines, if the Sound is initialized.
        /// </summary>
        public bool IsInitialized
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the Factory.
        /// </summary>
        public static SoundFactory Factory { private set; get; }

        /// <summary>
        /// Initializes a new Sound.
        /// </summary>
        internal Sound()
        {
            
        }
        /// <summary>
        /// Initializes a new Sound based on the resource file.
        /// </summary>
        /// <param name="file">The File.</param>
        internal Sound(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("The sound resource could not be located");
            }

            ResourcePath = file;
            IsInitialized = true;
        }
        /// <summary>
        /// Initializes a new Sound class.
        /// </summary>
        static Sound()
        {
            Factory = new SoundFactory();
        }
    }
}
