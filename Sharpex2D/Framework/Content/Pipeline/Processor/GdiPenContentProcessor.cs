using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.GDI;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class GdiPenContentProcessor : ContentProcessor<GdiPen>
    {
        /// <summary>
        ///     Initializes a new GdiPenContentProcessor class.
        /// </summary>
        public GdiPenContentProcessor()
            : base(new Guid("B28CF4D2-9F85-4CB1-878E-F7D274026E60"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>GdiPen.</returns>
        public override GdiPen ReadData(string filepath)
        {
            if (!filepath.EndsWith(".s2d"))
            {
                throw new FormatException("Specified file is not in *.s2d format.");
            }

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);
                if (typeof (GdiPen).FullName != binaryreader.ReadString())
                {
                    throw new FormatException("[GdiPenContentProcessor] Unable to read file format.");
                }

                try
                {
                    var gdiPen = new GdiPen
                    {
                        Width = binaryreader.ReadSingle(),
                        Color = Color.FromArgb(binaryreader.ReadByte(), binaryreader.ReadByte(),
                            binaryreader.ReadByte(), binaryreader.ReadByte())
                    };

                    return gdiPen;
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
        public override void WriteData(GdiPen data, string destinationpath)
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
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
    }
}