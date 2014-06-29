using System;
using System.IO;
using Sharpex2D.Framework.Rendering.DirectX11.Fonts;
using Sharpex2D.Framework.Rendering.Fonts;

namespace Sharpex2D.Framework.Content.Pipeline.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectXFontContentProcessor : ContentProcessor<DirectXFont>
    {
        /// <summary>
        ///     Initializes a new DirectXFontContentProcessor class.
        /// </summary>
        public DirectXFontContentProcessor()
            : base(new Guid("64C106BB-C87B-49AB-82A2-FE3AF5FEF54A"))
        {
        }

        /// <summary>
        ///     Reads the data.
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
        ///     Writes the data.
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