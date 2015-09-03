// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.


using System;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    public class Texture2D : IContent
    {
        internal readonly ITexture Texture;

        /// <summary>
        /// Initializes a new Texture2D class.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        internal Texture2D(ITexture texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// Initializes a new Texture2D class.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public Texture2D(int width, int height)
        {
            Texture = GameHost.SpriteBatch.Renderer.CreateTexture(width, height);
        }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width => Texture.Width;

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height => Texture.Height;

        /// <summary>
        /// A value indicating whether the texture is locked.
        /// </summary>
        public bool IsLocked => Texture.IsLocked;

        /// <summary>
        /// Gets or sets the color of the specified texel.
        /// </summary>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns>Color.</returns>
        /// <remarks>The texture must be locked before accessing the color data.</remarks>
        public Color this[int x, int y]
        {
            set
            {
                if (!IsLocked)
                    throw new InvalidOperationException("The texture must be locked before accessing the color data.");
                if (x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
                if (y >= Height) throw new ArgumentOutOfRangeException(nameof(y));
                Texture[x, y] = value;
            }
            get
            {
                if (!IsLocked)
                    throw new InvalidOperationException("The texture must be locked before accessing the color data.");
                if (x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
                if (y >= Height) throw new ArgumentOutOfRangeException(nameof(y));
                return Texture[x, y];
            }
        }

        /// <summary>
        /// Locks the texture.
        /// </summary>
        public void Lock()
        {
            if (IsLocked) throw new InvalidOperationException("The texture must be unlocked in order to be locked.");

            Texture.Lock();
        }

        /// <summary>
        /// Unlocks the data.
        /// </summary>
        public void Unlock()
        {
            if (!IsLocked) throw new InvalidOperationException("The texture must be locked in order to be unlocked.");

            Texture.Unlock();
        }
    }
}
