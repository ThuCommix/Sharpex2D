using System.Drawing;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Graphics Device Interface Texture")]
    public class GDITexture : Texture2D
    {
        #region Texture2D Implementation

        private readonly int _height;
        private readonly int _width;

        /// <summary>
        ///     Gets the Width.
        /// </summary>
        public override int Width
        {
            get { return _width; }
        }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        public override int Height
        {
            get { return _height; }
        }

        #endregion

        /// <summary>
        ///     Initializes a new GdiTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal GDITexture(Bitmap bitmap)
        {
            Bmp = bitmap;
            _width = Bmp.Width;
            _height = Bmp.Height;
        }

        /// <summary>
        ///     Gets the GdiTexture data.
        /// </summary>
        internal Bitmap Bmp { private set; get; }
    }
}