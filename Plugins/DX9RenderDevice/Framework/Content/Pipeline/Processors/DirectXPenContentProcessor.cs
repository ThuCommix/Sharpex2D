using System;
using System.IO;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.DirectX9;

namespace Sharpex2D.Framework.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectXPenContentProcessor : ContentProcessor<DirectXPen>
    {
        /// <summary>
        ///     Initializes a new DirectXPenContentProcessor class.
        /// </summary>
        public DirectXPenContentProcessor()
            : base(new Guid("7F6F71FA-A7D6-4858-BA5D-A0C67246F7B5"))
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