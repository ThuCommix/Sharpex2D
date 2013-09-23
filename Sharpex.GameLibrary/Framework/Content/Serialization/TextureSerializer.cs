using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SharpexGL.Framework.Common.Extensions;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Content.Serialization
{
    public class TextureSerializer : ContentSerializer<Texture>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override Texture Read(BinaryReader reader)
        {
            var stream = new MemoryStream(reader.ReadAllBytes());
            var newImage = Image.FromStream(stream);
            stream.Dispose();
            var texture = new Texture();
            texture.Texture2D = (Bitmap)newImage;
            reader.Close();
            return texture;
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, Texture value)
        {
            //Define final destination:
            var stream = new MemoryStream();
            value.Texture2D.Save(stream, ImageFormat.Png);
            var bytes = stream.ToArray();
            writer.Write(bytes);
            stream.Dispose();
            writer.Close();
        }
    }
}
