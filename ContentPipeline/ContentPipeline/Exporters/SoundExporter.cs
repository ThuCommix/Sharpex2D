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

using System.Globalization;
using System.IO;
using CSCore.Codecs;
using CSCore.Codecs.WAV;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Audio;
using Sharpex2D.Framework.Content;

namespace ContentPipeline.Exporters
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ExportContent(typeof(Sound))]
    public class SoundExporter : Exporter
    {
        /// <summary>
        /// Gets or sets the file filter.
        /// </summary>
        public override string[] FileFilter
        {
            get { return new[] { ".mp3", ".wav", ".flac" }; }
        }

        /// <summary>
        /// Raises when the xml content is ready for processing.
        /// </summary>
        /// <param name="inputPath">The InputPath.</param>
        /// <param name="xmlContent">The XmlContent.</param>
        public override void OnCreate(string inputPath, ref XmlContent xmlContent)
        {
            var targetStream = new MemoryStream();
            using (var stream = CodecFactory.Instance.GetCodec(inputPath))
            {
                using (var memoryStream = new WaveWriter(targetStream, stream.WaveFormat))
                {
                    byte[] buffer = new byte[stream.WaveFormat.BytesPerSecond];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, read);
                    }

                    xmlContent.SetDataStream(targetStream,
                        AttributeHelper.GetAttribute<ExportContentAttribute>(this).Type,
                        XmlContentCompression.Deflate);
                }
            }
            targetStream.Dispose();

            var artist = "";
            var title = new FileInfo(inputPath).Name.Split('.')[0];
            var album = "";
            var year = 0;
            /*
            var id3V1 = ID3v1.FromFile(inputPath);
            if (id3V1 != null)
            {
                if (String.IsNullOrWhiteSpace(id3V1.Title))
                {
                    title = id3V1.Title;
                }
                if (String.IsNullOrWhiteSpace(id3V1.Artist))
                {
                    artist = id3V1.Artist;
                }
                if (String.IsNullOrWhiteSpace(id3V1.Album))
                {
                    album = id3V1.Album;
                }

                if (id3V1.Year.HasValue)
                {
                    year = id3V1.Year.Value;
                }
            }

            var id3V2 = ID3v2.FromFile(inputPath);
            if (id3V2 != null)
            {
                if (String.IsNullOrWhiteSpace(id3V2.QuickInfo.Title))
                {
                    title = id3V2.QuickInfo.Title;
                }
                if (String.IsNullOrWhiteSpace(id3V2.QuickInfo.Artist))
                {
                    artist = id3V2.QuickInfo.Artist;
                }
                if (String.IsNullOrWhiteSpace(id3V2.QuickInfo.Album))
                {
                    album = id3V2.QuickInfo.Album;
                }

                if (id3V2.QuickInfo.Year.HasValue)
                {
                    year = id3V2.QuickInfo.Year.Value;
                }
            }*/

            xmlContent.Add(new XmlContentMetaData("Title", title));
            xmlContent.Add(new XmlContentMetaData("Artist", artist));
            xmlContent.Add(new XmlContentMetaData("Album", album));
            xmlContent.Add(new XmlContentMetaData("Year", year.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
