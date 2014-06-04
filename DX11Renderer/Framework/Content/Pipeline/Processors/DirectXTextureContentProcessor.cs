using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Rendering.DirectX11;

namespace Sharpex2D.Framework.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectXTextureContentProcessor : ContentProcessor<DirectXTexture>
    {
        /// <summary>
        /// Initializes a new DirectXTextureContentProcessor class.
        /// </summary>
        public DirectXTextureContentProcessor()
            : base(new Guid("E032D8BB-0D88-468A-A77D-FFE1563D3BDA"))
        {
        }
        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>DirectXTexture.</returns>
        public override DirectXTexture ReadData(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);

                var content = binaryreader.ReadAllBytes();

                binaryreader.Close();
                try
                {
                    using (var memoryStream = new MemoryStream(content))
                    {
                        return new DirectXTexture((Bitmap)Image.FromStream(memoryStream));
                    }
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
        /// <summary>
        /// Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(DirectXTexture data, string destinationpath)
        {
            using (var fileStream = new FileStream(destinationpath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var binaryWriter = new BinaryWriter(fileStream);

                    var memoryStream = new MemoryStream();
                    data.RawBitmap.Save(memoryStream, ImageFormat.Png);

                    var content = memoryStream.ToArray();
                    memoryStream.Close();

                    binaryWriter.Write(content, 0, content.Length);

                    binaryWriter.Close();
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
    }
}
