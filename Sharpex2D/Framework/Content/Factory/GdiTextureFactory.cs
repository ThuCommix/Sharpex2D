using System;
using System.Drawing;
using System.IO;
using Sharpex2D.Framework.Rendering.GDI;

namespace Sharpex2D.Framework.Content.Factory
{
    public class GdiTextureFactory : IFactory<GdiTexture>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType { get { return typeof (GdiTexture); }}
        /// <summary>
        /// Creates a new GdiTexture from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>GdiTexture</returns>
        public GdiTexture Create(string file)
        {
            return new GdiTexture((Bitmap) Image.FromFile(file));
        }
        /// <summary>
        /// Creates a new GdiTexture from the given Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>GdiTexture</returns>
        public GdiTexture Create(Stream stream)
        {
            return new GdiTexture((Bitmap)Image.FromStream(stream));
        }
    }
}
