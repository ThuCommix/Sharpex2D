using System;

namespace SharpexGL.Framework.Rendering.GDI
{
    public class GdiSpriteBuffer
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
            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiSpriteBuffer expects a GdiTexture as resource.");

            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _texture = gdiTexture;
        }
        /// <summary>
        /// Initializes a new GdiSpriteBuffer class.
        /// </summary>
        internal GdiSpriteBuffer()
        {

        }

        private GdiTexture _texture;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
    }
}
