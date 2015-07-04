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
using System.IO;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Rendering;

namespace ContentPipeline.Exporters
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ExportContent(typeof(Texture2D))]
    public class Texture2DExporter : Exporter
    {
        /// <summary>
        /// Gets or sets the file filter.
        /// </summary>
        public override string[] FileFilter
        {
            get { return new[] { ".png", ".bmp", ".jpg", ".jpeg", ".tiff" }; }
        }

        /// <summary>
        /// Raises when the xml content is ready for processing.
        /// </summary>
        /// <param name="inputPath">The InputPath.</param>
        /// <param name="xmlContent">The XmlContent.</param>
        public override void OnCreate(string inputPath, ref XmlContent xmlContent)
        {
            var bitmap = (Bitmap) Image.FromFile(inputPath);
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                xmlContent.SetData(memoryStream.ToArray(),
                    AttributeHelper.GetAttribute<ExportContentAttribute>(this).Type);
            }
        }
    }
}
