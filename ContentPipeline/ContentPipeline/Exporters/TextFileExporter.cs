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

using System.IO;
using System.Text;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Content;

namespace ContentPipeline.Exporters
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ExportContent(typeof(TextFile))]
    public class TextFileExporter : Exporter
    {
        /// <summary>
        /// Gets or sets the file filter.
        /// </summary>
        public override string[] FileFilter
        {
            get { return new[] { ".txt" }; }
        }

        /// <summary>
        /// Raises when the xml content is ready for processing.
        /// </summary>
        /// <param name="inputPath">The InputPath.</param>
        /// <param name="xmlContent">The XmlContent.</param>
        public override void OnCreate(string inputPath, ref XmlContent xmlContent)
        {
            xmlContent.Add(new XmlContentMetaData("Encoding", Encoding.UTF8.BodyName));

            xmlContent.SetData(Encoding.UTF8.GetBytes(File.ReadAllText(inputPath)),
                AttributeHelper.GetAttribute<ExportContentAttribute>(this).Type);
        }
    }
}
