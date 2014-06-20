using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Media.Video;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class VideoContentProcessor : ContentProcessor<Video>
    {
        public VideoContentProcessor()
            : base(new Guid("135FA99E-84D9-46CA-9C7E-CDE0772DF1EC"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>Video.</returns>
        public override Video ReadData(string filepath)
        {
            return new Video(filepath);
        }

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(Video data, string destinationpath)
        {
            try
            {
                File.Copy(data.ResourcePath, destinationpath);
            }
            catch (Exception ex)
            {
                throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
            }
        }
    }
}