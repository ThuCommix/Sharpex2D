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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Sharpex2D.Math;
using Rectangle = System.Drawing.Rectangle;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class OpenGLTexture : ITexture
    {
        private List<ColorData> _lockedColors;
        private byte[] _lockedData;

        /// <summary>
        /// Initializes a new OpenGLTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal OpenGLTexture(Bitmap bitmap)
        {
            Height = bitmap.Height;
            Width = bitmap.Width;

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            Id = OpenGLInterops.GenTexture();

            OpenGLInterops.BindTexture(OpenGLInterops.GL_TEXTURE_2D, Id);
            OpenGLInterops.TexParameterI(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_WRAP_S,
                (int) OpenGLInterops.GL_REPEAT);
            OpenGLInterops.TexParameterI(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_WRAP_T,
                (int) OpenGLInterops.GL_REPEAT);

            OpenGLInterops.TexParameterF(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_MIN_FILTER,
                SGL.SpriteBatch.InterpolationMode == InterpolationMode.Linear
                    ? OpenGLInterops.GL_LINEAR
                    : OpenGLInterops.GL_NEAREST);
            OpenGLInterops.TexParameterF(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_MAG_FILTER,
                SGL.SpriteBatch.InterpolationMode == InterpolationMode.Linear
                    ? OpenGLInterops.GL_LINEAR
                    : OpenGLInterops.GL_NEAREST);

            OpenGLInterops.TexImage2D(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_RGBA, Width, Height,
                OpenGLInterops.GL_BGRA, OpenGLInterops.GL_UNSIGNED_BYTE, data.Scan0);

            OpenGLInterops.BindTexture(OpenGLInterops.GL_TEXTURE_2D, 0);
            bitmap.UnlockBits(data);
            bitmap.Dispose();
        }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        internal uint Id { private set; get; }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// A value indicating whether the texture is locked.
        /// </summary>
        public bool IsLocked { private set; get; }

        /// <summary>
        /// Gets or sets the color of the specified texel.
        /// </summary>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns>Color.</returns>
        public Color this[int x, int y]
        {
            get
            {
                int offset = x*4 + y*(4*Width);

                return Color.FromArgb(_lockedData[offset + 3], _lockedData[offset], _lockedData[offset + 1],
                    _lockedData[offset + 2]);
            }
            set { _lockedColors.Add(new ColorData(value, new Vector2(x, y))); }
        }

        /// <summary>
        /// Locks the texture.
        /// </summary>
        public void Lock()
        {
            IsLocked = true;
            _lockedColors = new List<ColorData>();
            _lockedData = new byte[Width*Height*4];
            Bind();
            OpenGLInterops.GetTexImage(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_RGBA,
                OpenGLInterops.GL_UNSIGNED_BYTE, _lockedData);
            Unbind();
        }

        /// <summary>
        /// Unlocks the data.
        /// </summary>
        public void Unlock()
        {
            _lockedData = null;

            Bind();
            foreach (ColorData colordata in _lockedColors)
            {
                var pixelData = new byte[4];
                pixelData[0] = colordata.Color.R;
                pixelData[1] = colordata.Color.G;
                pixelData[2] = colordata.Color.B;
                pixelData[3] = colordata.Color.A;
                OpenGLInterops.TexSubImage2D(OpenGLInterops.GL_TEXTURE_2D, 0, (int) colordata.Position.X,
                    (int) colordata.Position.Y, 1, 1, OpenGLInterops.GL_RGBA,
                    OpenGLInterops.GL_UNSIGNED_BYTE, pixelData);
            }
            Unbind();

            IsLocked = false;
        }


        /// <summary>
        /// Binds the current texture.
        /// </summary>
        internal void Bind()
        {
            OpenGLInterops.ActiveTexture(OpenGLInterops.GL_TEXTURE0);
            OpenGLInterops.BindTexture(OpenGLInterops.GL_TEXTURE_2D, Id);
        }

        /// <summary>
        /// Unbinds the texture.
        /// </summary>
        internal void Unbind()
        {
            OpenGLInterops.BindTexture(OpenGLInterops.GL_TEXTURE_2D, 0);
        }
    }
}