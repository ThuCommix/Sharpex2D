using System;
using System.Drawing;

namespace SharpexGL.Framework.Rendering.Sprites.GDI
{
    public class SpriteBuffer
    {
        /// <summary>
        /// Initializes a new SpriteBuffer class.
        /// </summary>
        public SpriteBuffer()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            BufferData = null;
        }
        /// <summary>
        /// Sets or gets the X-Coord of the buffer.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// Sets or gets the Y-Coord of the buffer.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// Sets or gets the Width of the buffer.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// Sets or gets the height of the buffer.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// Sets or gets the buffer data.
        /// </summary>
        public Bitmap BufferData { set; private get; }

        /// <summary>
        /// Indicates whether the sprite was buffered.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>True if buffered</returns>
        public bool IsBuffered(int x, int y, int width, int height)
        {
            return x == X && y == Y && width == Width && height == Height && BufferData != null;
        }
        /// <summary>
        /// Gets the current buffer data.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBuffer()
        {
            if (BufferData == null) throw new InvalidOperationException("The SpriteBuffer is null.");
            return BufferData;
        }
        /// <summary>
        /// Sets the buffer.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="bufferData">The Bufferdata.</param>
        public void SetBuffer(int x, int y, int width, int height, Bitmap bufferData)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BufferData = bufferData;
        }
    }
}
