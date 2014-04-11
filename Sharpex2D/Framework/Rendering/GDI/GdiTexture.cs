using System.Drawing;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Factory;

namespace Sharpex2D.Framework.Rendering.GDI
{
    public class GdiTexture : ITexture, IContent
    {
        #region ITexture Implementation

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; private set; }

        #endregion

        /// <summary>
        /// Gets the GdiTextureFactory.
        /// </summary>
        public static GdiTextureFactory Factory { private set; get; }

        /// <summary>
        /// Initializes a new GdiTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal GdiTexture(Bitmap bitmap)
        {
            Bmp = bitmap;
            Width = Bmp.Width;
            Height = Bmp.Height;
        }

        /// <summary>
        /// Initializes a new GdiTexture class.
        /// </summary>
        static GdiTexture()
        {
            Factory = new GdiTextureFactory();
        }

        /// <summary>
        /// Gets the GdiTexture data.
        /// </summary>
        internal Bitmap Bmp { private set; get; }
    }
}
