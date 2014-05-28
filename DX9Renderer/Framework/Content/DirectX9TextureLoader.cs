using System;
using System.IO;
using Sharpex2D.Framework.Content.Serialization;
using Sharpex2D.Framework.Rendering.DirectX9;

namespace Sharpex2D.Framework.Content
{
    public class DirectX9TextureLoader : IContentExtension
    {
        /// <summary>
        /// Create the new DirectXTexture.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IContent Create(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var binReader = new BinaryReader(fileStream);
                var dxTexture = new DirectX9TextureSerializer().Read(binReader);
                binReader.Close();
                return dxTexture;
            }
        }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType
        {
            get { return typeof (DirectXTexture); }
        }
        /// <summary>
        /// Initializes a new DirectX9TextureLoader class.
        /// </summary>
        public DirectX9TextureLoader()
        {
            Guid = new Guid("192BD9FE-5846-4012-9FD2-AF3EF21EB8A1");
        }
    }
}
