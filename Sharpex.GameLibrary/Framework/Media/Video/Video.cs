using System.IO;
using SharpexGL.Framework.Content.Factory;

namespace SharpexGL.Framework.Media.Video
{
    public class Video
    {
        /// <summary>
        /// Sets or gets the ResourcePath.
        /// </summary>
        public string ResourcePath { get; private set; }
        /// <summary>
        /// A value indicating whether the Video is initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }
        /// <summary>
        /// Gets the Factory.
        /// </summary>
        public static VideoFactory Factory { get; private set; }
        /// <summary>
        /// Initializes a new Video class.
        /// </summary>
        internal Video()
        {
            
        }
        /// <summary>
        /// Initializes a new Video class.
        /// </summary>
        /// <param name="file">The File.</param>
        internal Video(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("The video resource could not be located");
            }

            IsInitialized = true;
            ResourcePath = file;
        }
        /// <summary>
        /// Initializes a new Video class.
        /// </summary>
        static Video()
        {
            Factory = new VideoFactory();
        }
    }
}
