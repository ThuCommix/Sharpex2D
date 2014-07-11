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
using Sharpex2D.Rendering.DirectX10;

namespace Sharpex2D.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DirectXPenContentProcessor : ContentProcessor<DirectXPen>
    {
        /// <summary>
        ///     Initializes a new DirectXPenContentProcessor class.
        /// </summary>
        public DirectXPenContentProcessor()
            : base(new Guid("F070E843-B522-4801-BD1F-B01C062BAC8E"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>DirectXPen.</returns>
        public override DirectXPen ReadData(string filepath)
        {
            if (!filepath.EndsWith(".s2d"))
            {
                throw new FormatException("Specified file is not in *.s2d format.");
            }

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);
                if (typeof (DirectXPen).FullName != binaryreader.ReadString())
                {
                    throw new FormatException("[DirectXPenContentProcessor] Unable to read file format.");
                }

                try
                {
                    var dxPen = new DirectXPen
                    {
                        Width = binaryreader.ReadSingle(),
                        Color = Color.FromArgb(binaryreader.ReadByte(), binaryreader.ReadByte(),
                            binaryreader.ReadByte(), binaryreader.ReadByte())
                    };

                    binaryreader.Close();

                    return dxPen;
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
        public override void WriteData(DirectXPen data, string destinationpath)
        {
            using (var fileStream = new FileStream(destinationpath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var binaryWriter = new BinaryWriter(fileStream);
                    binaryWriter.Write(data.GetType().FullName);

                    binaryWriter.Write(data.Width);
                    binaryWriter.Write(data.Color.A);
                    binaryWriter.Write(data.Color.R);
                    binaryWriter.Write(data.Color.G);
                    binaryWriter.Write(data.Color.B);

                    binaryWriter.Close();
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
    }
}