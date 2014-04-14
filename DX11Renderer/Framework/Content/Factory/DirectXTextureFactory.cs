using System;
using System.Drawing;
using System.IO;
using Sharpex2D.Framework.Rendering.DirectX;

namespace Sharpex2D.Framework.Content.Factory
{
    public class DirectXTextureFactory : IFactory<DirectXTexture>
    {
        /// <summary>
        /// Gets the Type.
        /// </summary>
        public Type FactoryType {
            get { return typeof (DirectXTexture); }
        }
        /// <summary>
        /// Creates a new DirectXTexture.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>DirectXTexture.</returns>
        public DirectXTexture Create(string file)
        {
            return new DirectXTexture((Bitmap)Image.FromFile(file));
        }
        /// <summary>
        /// Creates a new DirectXTexture.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>DirectXTexture.</returns>
        public DirectXTexture Create(Stream stream)
        {
            return new DirectXTexture((Bitmap)Image.FromStream(stream));
        }
    }
}
