using System;
using System.Drawing;

namespace SharpexGL.Framework.Rendering.GDI
{
    public class GdiSpriteSheet : ISpriteSheet
    {
        #region ISpriteSheet Implementation
        /// <summary>
        /// Gets texture on which the SpriteSheet is based.
        /// </summary>
        public ITexture Texture { get; private set; }

        /// <summary>
        /// Gets a texture based on location and size.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>ITexture</returns>
        public ITexture GetSprite(int x, int y, int width, int height)
        {
            var gdiTexture = Texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiSpriteSheet expects a GdiTexture as resource.");

            if (_buffer.IsBuffered(x, y, width, height))
            {
                return _buffer.GetBuffer();
            }

            _buffer.SetBuffer(x, y, width, height,
                new GdiTexture(_bmp.Clone(new Rectangle(x, y, width, height),
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb
                    )));


            return _buffer.GetBuffer();
        }

        #endregion

        /// <summary>
        /// Initializes a new GdiSpriteSheet class.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        public GdiSpriteSheet(ITexture texture)
        {
            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiSpriteSheet expects a GdiTexture as resource.");

            Texture = gdiTexture;
            _bmp = (Bitmap) gdiTexture.Bmp.Clone();
            _buffer = new GdiSpriteBuffer();
        }

        private readonly GdiSpriteBuffer _buffer;
        private readonly Bitmap _bmp;
    }
}
