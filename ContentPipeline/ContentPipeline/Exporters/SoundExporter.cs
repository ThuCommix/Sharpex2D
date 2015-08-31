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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CSCore;
using CSCore.Codecs;
using CSCore.Codecs.WAV;
using CSCore.Streams;
using CSCore.Tags.ID3;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Audio;
using Sharpex2D.Framework.Content;

namespace ContentPipeline.Exporters
{
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
        /// Raises when the content should be created.
        /// </summary>
        /// <param name="inputPath">The InputPath.</param>
        /// <param name="stream">The OutputStream.</param>
        /// <returns>The MetaInformations</returns>
        public override IEnumerable<MetaInformation> OnCreate(string inputPath, Stream stream)
        {
            var metaInfos = new List<MetaInformation>();
            var waveSource = CodecFactory.Instance.GetCodec(inputPath);
            if (waveSource.WaveFormat.Channels == 1)
                waveSource = new MonoToStereoSource(waveSource).ToWaveSource();
            using (var targetStream = new MemoryStream())
            {
                using (var memoryStream = new WaveWriter(targetStream, waveSource.WaveFormat))
                {
                    byte[] buffer = new byte[waveSource.WaveFormat.BytesPerSecond];
                    int read;
                    while ((read = waveSource.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, read);
                    }
                    targetStream.Seek(0, SeekOrigin.Begin);
                    targetStream.CopyTo(stream);
                }
            }

            waveSource.Dispose();

            var artist = "";
            var title = new FileInfo(inputPath).Name.Split('.')[0];
            var album = "";
            var year = 0;

            var id3V1 = ID3v1.FromFile(inputPath);
            if (id3V1 != null)
            {
                if (!String.IsNullOrWhiteSpace(id3V1.Title))
                {
                    title = id3V1.Title;
                }
                if (!String.IsNullOrWhiteSpace(id3V1.Artist))
                {
                    artist = id3V1.Artist;
                }
                if (!String.IsNullOrWhiteSpace(id3V1.Album))
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
                if (!String.IsNullOrWhiteSpace(id3V2.QuickInfo.Title))
                {
                    title = id3V2.QuickInfo.Title;
                }
                if (!String.IsNullOrWhiteSpace(id3V2.QuickInfo.Artist))
                {
                    artist = id3V2.QuickInfo.Artist;
                }
                if (!String.IsNullOrWhiteSpace(id3V2.QuickInfo.Album))
                {
                    album = id3V2.QuickInfo.Album;
                }

                if (id3V2.QuickInfo.Year.HasValue)
                {
                    year = id3V2.QuickInfo.Year.Value;
                }
            }

            metaInfos.Add(new MetaInformation("Title", title));
            metaInfos.Add(new MetaInformation("Artist", artist));
            metaInfos.Add(new MetaInformation("Album", album));
            metaInfos.Add(new MetaInformation("Year", year.ToString(CultureInfo.InvariantCulture)));
            return metaInfos;
        }
    }
}

