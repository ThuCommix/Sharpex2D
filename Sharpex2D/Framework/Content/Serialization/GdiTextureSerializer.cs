using System.Drawing;
using System.IO;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Rendering.GDI;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class GdiTextureSerializer : ContentSerializer<GdiTexture>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override GdiTexture Read(BinaryReader reader)
        {
            var stream = new MemoryStream(reader.ReadAllBytes());
            var newImage = (Bitmap)Image.FromStream(stream);
            stream.Dispose();
            reader.Close();
            return new GdiTexture(newImage);
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, GdiTexture value)
        {
            var stream = new MemoryStream();
            value.Bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var bytes = stream.ToArray();
            writer.Write(bytes);
            stream.Dispose();
            writer.Close();
        }
    }
}
