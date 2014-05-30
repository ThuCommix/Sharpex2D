using System;
using System.Drawing;
using System.IO;
using Sharpex2D.Framework.Rendering.DirectX10;

namespace Sharpex2D.Framework.Content.Factory
{
    public class DirectX10TextureFactory : IFactory<DirectXTexture>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType { get { return typeof(DirectXTexture); } }
        /// <summary>
        /// Creates the DirectXTexture.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>DirectXTexture.</returns>
        public DirectXTexture Create(string file)
        {
            return new DirectXTexture((Bitmap) Image.FromFile(file));
        }
        /// <summary>
        /// Creates the DirectXTexture.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>DirectXTexture.</returns>
        public DirectXTexture Create(Stream stream)
        {
            return new DirectXTexture((Bitmap)Image.FromStream(stream));
        }
    }
}
