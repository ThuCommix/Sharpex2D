using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SharpexGL.Framework.Common.Extensions;
using SharpexGL.Framework.Rendering.Sprites;

namespace SharpexGL.Framework.Content.Serialization
{
    public class AnimationSerializer : ContentSerializer<Animation>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override Animation Read(BinaryReader reader)
        {
            var stream = new MemoryStream(reader.ReadAllBytes());
            var newImage = (Bitmap)Image.FromStream(stream);
            stream.Dispose();
            var spriteSheet = new SpriteSheet(newImage);
            var duration = reader.ReadSingle();
            var keyFrames = reader.ReadInt32();
            var rect = new Math.Rectangle(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            reader.Close();
            return new Animation(duration, keyFrames, spriteSheet, rect);
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, Animation value)
        {
            //write image
            var stream = new MemoryStream();
            value.Texture.Texture2D.Save(stream, ImageFormat.Png);
            var bytes = stream.ToArray();
            writer.Write(bytes);
            //write duration
            writer.Write(value.Duration);
            //write keyframe amount
            writer.Write(value.KeyFrames);
            //write rectangle
            writer.Write(value.Rect.X);
            writer.Write(value.Rect.Y);
            writer.Write(value.Rect.Width);
            writer.Write(value.Rect.Height);
            //cleaning
            stream.Dispose();
            writer.Close();
        }
    }
}
