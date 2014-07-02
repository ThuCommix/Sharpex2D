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
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Rendering.Fonts;
using Sharpex2D.Framework.Rendering.GDI.Fonts;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class GDIFontContentProcessor : ContentProcessor<GDIFont>
    {
        public GDIFontContentProcessor()
            : base(new Guid("CF086145-883B-49F4-B76E-655793B9C543"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>GdiFont.</returns>
        public override GDIFont ReadData(string filepath)
        {
            if (!filepath.EndsWith(".s2d"))
            {
                throw new FormatException("Specified file is not in *.s2d format.");
            }

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);
                if (typeof (Typeface).FullName != binaryreader.ReadString() ||
                    typeof (GDIFont).FullName != binaryreader.ReadString())
                {
                    throw new FormatException("[GdiFontContentProcessor] Unable to read file format.");
                }

                try
                {
                    var typeface = new Typeface
                    {
                        FamilyName = binaryreader.ReadString(),
                        Size = binaryreader.ReadSingle(),
                        Style = (TypefaceStyle) binaryreader.ReadInt32()
                    };

                    return new GDIFont(typeface);
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(GDIFont data, string destinationpath)
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
            }
        }
    }
}