using System.Drawing;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Graphics Device Interface Texture")]
    public class GdiTexture : ITexture
    {
        #region ITexture Implementation

        /// <summary>
        ///     Gets the Width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        public int Height { get; private set; }

        #endregion

        /// <summary>
        ///     Initializes a new GdiTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal GdiTexture(Bitmap bitmap)
        {
            Bmp = bitmap;
            Width = Bmp.Width;
            Height = Bmp.Height;
        }

        /// <summary>
        ///     Gets the GdiTexture data.
        /// </summary>
        internal Bitmap Bmp { private set; get; }
    }
}