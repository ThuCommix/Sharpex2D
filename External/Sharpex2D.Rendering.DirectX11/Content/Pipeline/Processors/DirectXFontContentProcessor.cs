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
using System.IO;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.DirectX11;

namespace Sharpex2D.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DirectXFontContentProcessor : ContentProcessor<DirectXFont>
    {
        /// <summary>
        /// Initializes a new DirectXFontContentProcessor class.
        /// </summary>
        public DirectXFontContentProcessor()
            : base(new Guid("64C106BB-C87B-49AB-82A2-FE3AF5FEF54A"))
        {
        }

        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>DirectXFont.</returns>
        public override DirectXFont ReadData(string filepath)
        {
            if (!filepath.EndsWith(".s2d"))
            {
                throw new FormatException("Specified file is not in *.s2d format.");
            }

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);
                if (typeof (Typeface).FullName != binaryreader.ReadString() ||
                    typeof (DirectXFont).FullName != binaryreader.ReadString())
                {
                    throw new FormatException("[DirectXFontContentProcessor] Unable to read file format.");
                }

                try
                {
                    var typeface = new Typeface
                    {
                        FamilyName = binaryreader.ReadString(),
                        Size = binaryreader.ReadSingle(),
                        Style = (TypefaceStyle) binaryreader.ReadInt32()
                    };
                    binaryreader.Close();

                    return new DirectXFont(typeface);
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
        public override void WriteData(DirectXFont data, string destinationpath)
        {
            using (var fileStream = new FileStream(destinationpath, FileMode.Create, FileAccess.Write))
            {
                var binaryWriter = new BinaryWriter(fileStream);
                Typeface typeface = data.Typeface;

                try
                {
                    binaryWriter.Write(typeface.GetType().FullName);
                    binaryWriter.Write(data.GetType().FullName);

                    binaryWriter.Write(typeface.FamilyName);
                    binaryWriter.Write(typeface.Size);
                    binaryWriter.Write((int) typeface.Style);
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }

                binaryWriter.Close();
            }
        }
    }
}