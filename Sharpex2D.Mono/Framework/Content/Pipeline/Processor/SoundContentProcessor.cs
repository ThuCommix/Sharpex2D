using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Media.Sound;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class SoundContentProcessor : ContentProcessor<Sound>
    {
        /// <summary>
        ///     Initializes a new SoundContentProcessor class.
        /// </summary>
        public SoundContentProcessor()
            : base(new Guid("3F5D0CE8-26B8-4034-8C3C-2F0DB2D6F25A"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>Sound.</returns>
        public override Sound ReadData(string filepath)
        {
            return new Sound(filepath);
        }

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(Sound data, string destinationpath)
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