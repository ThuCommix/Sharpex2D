// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Content.Importers
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ImportContent(typeof(SpriteFont))]
    public class SpriteFontImporter : Importer
    {
        /// <summary>
        /// Raises when the xml content is loaded and ready for processing.
        /// </summary>
        /// <param name="xmlContent">The XmlContent.</param>
        /// <returns>IContent.</returns>
        public override IContent OnCreate(XmlContent xmlContent)
        {
            var fontName = xmlContent.First(x => x.Name == "FontName").Value;
            var fontSize = float.Parse(xmlContent.First(x => x.Name == "FontSize").Value,
                CultureInfo.InvariantCulture);
            var styleString = xmlContent.First(x => x.Name == "Style").Value;
            var spacing = float.Parse(xmlContent.First(x => x.Name == "Spacing").Value,
                CultureInfo.InvariantCulture);
            var kerning = xmlContent.First(x => x.Name == "Kerning").Value == "true";
            var style = SpriteFont.FontStyle.Regular;
            var offset = long.Parse(xmlContent.First(x => x.Name == "Offset").Value);

            switch (styleString)
            {
                case "Italic":
                    style = SpriteFont.FontStyle.Italic;
                    break;
                case "Bold":
                    style = SpriteFont.FontStyle.Bold;
                    break;
            }

            var charLayout = new Dictionary<char, Rectangle>();
            var data = xmlContent.GetData();

            using (var memoryStream = new MemoryStream(data))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    while (memoryStream.Position < offset)
                    {
                        char character = binaryReader.ReadChar();
                        var rectangle = new Rectangle(binaryReader.ReadInt32(), binaryReader.ReadInt32(),
                            binaryReader.ReadInt32(), binaryReader.ReadInt32());
                        if (!charLayout.ContainsKey(character))
                        {
                            charLayout.Add(character, rectangle);
                        }
                    }
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(data, (int) offset, (int) (data.Length - offset));
                var texture = new Texture2D(GameHost.SpriteBatch.Renderer.CreateTexture(memoryStream));
                return new SpriteFont(fontName, fontSize, spacing, kerning, style, texture, charLayout);
            }
        }
    }
}
