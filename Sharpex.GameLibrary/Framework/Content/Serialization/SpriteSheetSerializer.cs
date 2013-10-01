using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SharpexGL.Framework.Common.Extensions;
using SharpexGL.Framework.Rendering.Sprites;

namespace SharpexGL.Framework.Content.Serialization
{
    public class SpriteSheetSerializer : ContentSerializer<SpriteSheet>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override SpriteSheet Read(BinaryReader reader)
        {
            var stream = new MemoryStream(reader.ReadAllBytes());
            var newImage = (Bitmap)Image.FromStream(stream);
            stream.Dispose();
            reader.Close();
            return new SpriteSheet(newImage);
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, SpriteSheet value)
        {
            //save the image
            var stream = new MemoryStream();
            value.RawTexture.Save(stream, ImageFormat.Png);
            var bytes = stream.ToArray();
            writer.Write(bytes);
            stream.Dispose();
            writer.Close();
        }
    }
}
