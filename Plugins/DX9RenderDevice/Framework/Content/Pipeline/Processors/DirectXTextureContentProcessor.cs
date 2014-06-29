using System;
using System.Drawing.Imaging;
using System.IO;
using Sharpex2D.Framework.Rendering.DirectX9;

namespace Sharpex2D.Framework.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectXTextureContentProcessor : ContentProcessor<DirectXTexture>
    {
        /// <summary>
        ///     Initializes a new DirectXTextureContentProcessor class.
        /// </summary>
        public DirectXTextureContentProcessor()
            : base(new Guid("B13691DB-CF30-4A9A-A670-7030D2B798FD"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>DirectXTexture.</returns>
        public override DirectXTexture ReadData(string filepath)
        {
            try
            {
                return new DirectXTexture(filepath);
            }
            catch (Exception ex)
            {
                throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
            }
        }

        /// <summary>
        ///     Writes the data.
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

                    byte[] content = memoryStream.ToArray();
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