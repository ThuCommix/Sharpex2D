using System;
using System.IO;
using SharpexGL.Framework.Content.Factory;

namespace SharpexGL.Framework.Media.Sound
{
    public class Sound
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
                throw new FileNotFoundException("The soundresource could not be located");
            }
            if (file.ToLower().EndsWith("mp3") | file.ToLower().EndsWith("wav") | file.ToLower().EndsWith("wma") | file.ToLower().EndsWith("flac"))
            {
                ResourcePath = file;
                IsInitialized = true;
                return;
            }
            throw new FormatException("Could not read format, allowed: mp3, wav, wma, flac");
        }

        static Sound()
        {
            Factory = new SoundFactory();
        }
    }
}
