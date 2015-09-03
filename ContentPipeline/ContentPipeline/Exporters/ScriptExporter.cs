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

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Scripting;

namespace ContentPipeline.Exporters
{
    [ExportContent(typeof(Script))]
    public class ScriptExporter : Exporter
    {
        /// <summary>
        /// Gets or sets the file filter.
        /// </summary>
        public override string[] FileFilter => new[] {".cs", ".vb"};

        /// <summary>
        /// Raises when the content should be created.
        /// </summary>
        /// <param name="inputPath">The InputPath.</param>
        /// <param name="stream">The OutputStream.</param>
        /// <returns>The MetaInformations</returns>
        public override IEnumerable<MetaInformation> OnCreate(string inputPath, Stream stream)
        {
            var metaInfos = new List<MetaInformation>();
            var fileInfo = new FileInfo(inputPath);
            metaInfos.Add(new MetaInformation("Type",
                fileInfo.Extension == FileFilter[0]
                    ? ((int) ScriptType.CSharp).ToString(CultureInfo.InvariantCulture)
                    : ((int) ScriptType.VisualBasic).ToString(CultureInfo.InvariantCulture)));

            metaInfos.Add(new MetaInformation("Encoding", Encoding.UTF8.BodyName));

            var data = Encoding.UTF8.GetBytes(File.ReadAllText(inputPath));
            stream.Write(data, 0, data.Length);
            return metaInfos;
        }
    }
}

