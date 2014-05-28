using System.Drawing;
using System.IO;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Rendering.DirectX11;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class DirectX11TextureSerializer : ContentSerializer<DirectXTexture>
    {
        public override DirectXTexture Read(BinaryReader reader)
        {
            using (var mStream = new MemoryStream(reader.ReadAllBytes()))
            {
                reader.Close();
                return new DirectXTexture((Bitmap) Image.FromStream(mStream));
            }
        }

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
