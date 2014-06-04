using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Rendering.GDI;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class GdiTextureContentProcessor : ContentProcessor<GdiTexture>
    {
        public GdiTextureContentProcessor()
            : base(new Guid("E36EBED0-81E6-47D1-9AC7-CE7210360989"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>GdiTexture.</returns>
        public override GdiTexture ReadData(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);

                byte[] content = binaryreader.ReadAllBytes();

                try
                {
                    using (var memoryStream = new MemoryStream(content))
                    {
                        return new GdiTexture((Bitmap) Image.FromStream(memoryStream));
                    }
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(GdiTexture data, string destinationpath)
        {
            using (var fileStream = new FileStream(destinationpath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var binaryWriter = new BinaryWriter(fileStream);

                    var memoryStream = new MemoryStream();


                    data.Bmp.Save(memoryStream, ImageFormat.Png);

                    byte[] content = memoryStream.ToArray();
                    memoryStream.Dispose();

                    binaryWriter.Write(content, 0, content.Length);
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
    }
}