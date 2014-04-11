using System;

namespace Sharpex2D.Framework.Rendering.DirectX
{
    public class DirectXSpriteBuffer
    {
        /// <summary>
        /// A value indicating whether, the requested Sprite is buffered.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>True if buffered</returns>
        public bool IsBuffered(int x, int y, int width, int height)
        {
            return _x == x && _y == y && _width == width && _height == height && _texture != null;
        }
        /// <summary>
        /// Gets the Buffer.
        /// </summary>
        /// <returns>ITexture</returns>
        public ITexture GetBuffer()
        {
            if (_texture == null) throw new InvalidOperationException("The buffered texture is null.");

            return _texture;
        }
        /// <summary>
        /// Sets the Buffer.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="texture">The Texture.</param>
        public void SetBuffer(int x, int y, int width, int height, ITexture texture)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXSpriteBuffer expects a DirectXTexture as resource.");

            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _texture = dxTexture;
        }
        /// <summary>
        /// Initializes a new DirectXSpriteBuffer class.
        /// </summary>
        internal DirectXSpriteBuffer()
        {

        }

        private DirectXTexture _texture;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
    }
}
