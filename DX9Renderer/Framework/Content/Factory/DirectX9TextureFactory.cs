using System;
using System.IO;
using Sharpex2D.Framework.Rendering.DirectX9;

namespace Sharpex2D.Framework.Content.Factory
{
    public class DirectX9TextureFactory : IFactory<DirectXTexture>
    {
        /// <summary>
        /// Gets the Factory type.
        /// </summary>
        public Type FactoryType { get { return typeof (DirectXTexture); } }
        /// <summary>
        /// Creates a new DirectXTexture.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>DirectXTexture.</returns>
        public DirectXTexture Create(string file)
        {
            return new DirectXTexture(file);
        }
        /// <summary>
        /// Creates a new DirectXTexture.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public DirectXTexture Create(Stream stream)
        {
            return new DirectXTexture(stream);
        }
    }
}
