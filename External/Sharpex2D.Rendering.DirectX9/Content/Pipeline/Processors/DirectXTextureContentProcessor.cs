// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Drawing.Imaging;
using System.IO;
using Sharpex2D.Rendering.DirectX9;

namespace Sharpex2D.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DirectXTextureContentProcessor : ContentProcessor<DirectXTexture>
    {
        /// <summary>
        /// Initializes a new DirectXTextureContentProcessor class.
        /// </summary>
        public DirectXTextureContentProcessor()
            : base(new Guid("B13691DB-CF30-4A9A-A670-7030D2B798FD"))
        {
        }

        /// <summary>
        /// Reads the data.
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