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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Rendering;
using Rectangle = System.Drawing.Rectangle;

namespace ContentPipeline.Exporters
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ExportContent(typeof(SpriteFont))]
    public class SpriteFontExporter : Exporter
    {
        /// <summary>
        /// Gets or sets the file filter.
        /// </summary>
        public override string[] FileFilter
        {
            get { return new[] { ".font" }; }
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
            XDocument xml = XDocument.Load(inputPath);
            string fontName = xml.Elements().Select(x => x.Element("FontName")).First().Value;
            float fontSize = float.Parse(xml.Elements().Select(x => x.Element("Size")).First().Value,
                CultureInfo.InvariantCulture.NumberFormat);
            float spacing = float.Parse(xml.Elements().Select(x => x.Element("Spacing")).First().Value,
                CultureInfo.InvariantCulture.NumberFormat);
            string style = xml.Elements().Select(x => x.Element("Style")).First().Value;
            bool useKerning = xml.Elements().Select(x => x.Element("UseKerning")).First().Value == "True";

            IEnumerable<XElement> result = xml.Elements().Select(x => x.Element("CharacterRegions"));
            var fontStyle = FontStyle.Regular;

            switch (style)
            {
                case "Bold":
                    fontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    fontStyle = FontStyle.Italic;
                    break;
            }

            //resharper going crazy lol
            List<Tuple<short, short>> regions = (from charRegion in result
                                                 let start = short.Parse(charRegion.Elements().Select(x => x.Element("Start")).First().Value)
                                                 let end = short.Parse(charRegion.Elements().Select(x => x.Element("End")).First().Value)
                                                 select new Tuple<short, short>(start, end)).ToList();

            var characters = new List<short>();
            foreach (Tuple<short, short> region in regions)
            {
                for (short i = region.Item1; i <= region.Item2; i++)
                {
                    if (!characters.Contains(i))
                        characters.Add(i);
                }
            }

            var targetFont = new Font(fontName, fontSize, fontStyle);

            //measuring dimensions
            const float maxWidth = 512; //max width
            float currentWidth = 0;
            float height = 0;
            float highestHeight = 0;

            var testBmp = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(testBmp);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            float padding = graphics.MeasureString("__", targetFont).Width - 2;
            var fontDescriptions = new Dictionary<char, Rectangle>();

            foreach (short character in characters)
            {
                SizeF dimension =
                    graphics.MeasureString("_" + (char)(character) + "_", targetFont);
                float charWidth = dimension.Width - padding + spacing;
                if (dimension.Height > highestHeight)
                    highestHeight = dimension.Height;

                if (currentWidth + charWidth > maxWidth)
                {
                    currentWidth = 0;
                    height += highestHeight + 10;
                }

                fontDescriptions.Add((char)character,
                    new Rectangle((int)Math.Ceiling(currentWidth), (int)Math.Ceiling(height),
                        (int)Math.Ceiling(charWidth),
                        (int)Math.Ceiling(dimension.Height)));
                currentWidth += charWidth;
            }

            height += highestHeight;

            graphics.Dispose();
            testBmp.Dispose();

            var fontBitmap = new Bitmap((int)Math.Ceiling(maxWidth), (int)Math.Ceiling(height));
            graphics = Graphics.FromImage(fontBitmap);
            graphics.Clear(System.Drawing.Color.Transparent);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            foreach (KeyValuePair<char, Rectangle> fontDescription in fontDescriptions)
            {
                graphics.DrawString(fontDescription.Key.ToString(CultureInfo.InvariantCulture), targetFont,
                    Brushes.White,
                    fontDescription.Value);
            }

            metaInfos.Add(new MetaInformation("FontName", fontName));
            metaInfos.Add(new MetaInformation("FontSize", fontSize.ToString(CultureInfo.InvariantCulture)));
            metaInfos.Add(new MetaInformation("Style", style));
            metaInfos.Add(new MetaInformation("Spacing", spacing.ToString(CultureInfo.InvariantCulture)));
            metaInfos.Add(new MetaInformation("Kerning", useKerning ? "True" : "False"));

            using (var outputStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(outputStream))
                {
                    foreach (KeyValuePair<char, Rectangle> fontDescription in fontDescriptions)
                    {
                        binaryWriter.Write(fontDescription.Key);
                        binaryWriter.Write(fontDescription.Value.X);
                        binaryWriter.Write(fontDescription.Value.Y);
                        binaryWriter.Write(fontDescription.Value.Width);
                        binaryWriter.Write(fontDescription.Value.Height);
                    }

                    metaInfos.Add(new MetaInformation("Offset", outputStream.Position.ToString(CultureInfo.InvariantCulture)));

                    fontBitmap.Save(outputStream, ImageFormat.Png);
                    fontBitmap.Dispose();
                    graphics.Dispose();
                    outputStream.Seek(0, SeekOrigin.Begin);
                    outputStream.CopyTo(stream);
                }
            }

            return metaInfos;
        }
    }
}
