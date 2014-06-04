using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Rendering.Font;
using Sharpex2D.Framework.Rendering.GDI;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class GdiFontContentProcessor : ContentProcessor<GdiFont>
    {
        public GdiFontContentProcessor()
            : base(new Guid("CF086145-883B-49F4-B76E-655793B9C543"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>GdiFont.</returns>
        public override GdiFont ReadData(string filepath)
        {
            if (!filepath.EndsWith(".s2d"))
            {
                throw new FormatException("Specified file is not in *.s2d format.");
            }

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);
                if (typeof (Typeface).FullName != binaryreader.ReadString() ||
                    typeof (GdiFont).FullName != binaryreader.ReadString())
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

                    return new GdiFont(typeface);
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
        public override void WriteData(GdiFont data, string destinationpath)
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