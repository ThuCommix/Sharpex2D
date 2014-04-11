using System;
using System.IO;
using Sharpex2D.Framework.Content.Serialization;
using Sharpex2D.Framework.Rendering.DirectX;

namespace Sharpex2D.Framework.Content
{
    internal class DirectXTextureLoader : IContentExtension
    {
        public IContent Create(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var binReader = new BinaryReader(fileStream);
                var dxTexture = new DirectXTextureSerializer().Read(binReader);
                binReader.Close();
                return dxTexture;
            }
        }

        public Guid Guid { get; private set; }
        public Type ContentType { get { return typeof (DirectXTexture); } }

        public DirectXTextureLoader()
        {
            Guid = new Guid("48BF7223-2BEA-4CA2-B9D8-E110FD1EC904");
        }
    }
}
