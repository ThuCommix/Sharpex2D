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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Common.Extensions;
using Sharpex2D.Rendering.GDI;

namespace Sharpex2D.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class GDITextureContentProcessor : ContentProcessor<GDITexture>
    {
        public GDITextureContentProcessor()
            : base(new Guid("E36EBED0-81E6-47D1-9AC7-CE7210360989"))
        {
        }

        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>GdiTexture.</returns>
        public override GDITexture ReadData(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);

                byte[] content = binaryreader.ReadAllBytes();

                try
                {
                    using (var memoryStream = new MemoryStream(content))
                    {
                        return new GDITexture((Bitmap) Image.FromStream(memoryStream));
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
        public override void WriteData(GDITexture data, string destinationpath)
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