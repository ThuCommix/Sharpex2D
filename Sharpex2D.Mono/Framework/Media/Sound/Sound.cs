using System.IO;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Media.Sound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Sound")]
    public class Sound : IContent
    {
        /// <summary>
        ///     Initializes a new Sound.
        /// </summary>
        internal Sound()
        {
        }

        /// <summary>
        ///     Initializes a new Sound based on the resource file.
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
        ///     Sets or Gets the resource path.
        /// </summary>
        public string ResourcePath { get; private set; }

        /// <summary>
        ///     Determines, if the Sound is initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }
    }
}