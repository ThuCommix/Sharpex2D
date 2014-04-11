using System;
using System.Drawing;

namespace Sharpex2D.Framework.Rendering.DirectX
{
    public class DirectXSpriteSheet : ISpriteSheet
    {
        #region ISpriteSheet Implementation
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
            var dxTexture = Texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXSpriteSheet expects a DirectXTexture as resource.");

            if (_buffer.IsBuffered(x, y, width, height))
            {
                return _buffer.GetBuffer();
            }

            _buffer.SetBuffer(x, y, width, height,
                new DirectXTexture(_bmp.Clone(new Rectangle(x, y, width, height),
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb
                    )));


            return _buffer.GetBuffer();
        }
        /// <summary>
        /// Gets texture on which the SpriteSheet is based.
        /// </summary>
        public ITexture Texture { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new DirectXSpriteSheet class.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        public DirectXSpriteSheet(ITexture texture)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null)
                throw new ArgumentException("DirectXSpriteSheet expected a DirectXTexture as resource.");

            Texture = dxTexture;
            _bmp = (Bitmap) dxTexture.RawBitmap.Clone();
            _buffer = new DirectXSpriteBuffer();
        }

        private readonly Bitmap _bmp;
        private readonly DirectXSpriteBuffer _buffer;
    }
}
