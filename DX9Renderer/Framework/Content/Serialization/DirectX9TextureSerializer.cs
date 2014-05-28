using System.IO;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Rendering.DirectX9;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class DirectX9TextureSerializer : ContentSerializer<DirectXTexture>
    {
        /// <summary>
        /// Reads the DirectXTexture.
        /// </summary>
        /// <param name="reader">The Reader.</param>
        /// <returns>DirectXTexture.</returns>
        public override DirectXTexture Read(BinaryReader reader)
        {
            using (var mStream = new MemoryStream(reader.ReadAllBytes()))
            {
                reader.Close();
                return new DirectXTexture(mStream);
            }
        }
        /// <summary>
        /// Writes the DirectXTexture.
        /// </summary>
        /// <param name="writer">The Writer.</param>
        /// <param name="value">The DirectXTexture.</param>
        public override void Write(BinaryWriter writer, DirectXTexture value)
        {
            var stream = new MemoryStream();
            value.RawBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var bytes = stream.ToArray();
            writer.Write(bytes);
            stream.Dispose();
            writer.Close();
        }
    }
}
