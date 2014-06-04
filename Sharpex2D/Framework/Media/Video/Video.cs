using System.IO;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Media.Video
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Video")]
    public class Video : IContent
    {
        /// <summary>
        ///     Initializes a new Video class.
        /// </summary>
        internal Video()
        {
        }

        /// <summary>
        ///     Initializes a new Video class.
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
        ///     Sets or gets the ResourcePath.
        /// </summary>
        public string ResourcePath { get; private set; }

        /// <summary>
        ///     A value indicating whether the Video is initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }
    }
}