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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal class GLTexture : ITexture
    {
        private List<ColorData> _lockedColors;
        private byte[] _lockedData;

        /// <summary>
        /// Initializes a new GLTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal GLTexture(Bitmap bitmap)
        {
            Height = bitmap.Height;
            Width = bitmap.Width;

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            Id = GLInterops.GenTexture();

            GLInterops.BindTexture(TextureParam.Texture2D, Id);
            GLInterops.TexParameterI(TextureParam.Texture2D, TextureParam.WrapS,
                (int) TextureParam.Repeat);
            GLInterops.TexParameterI(TextureParam.Texture2D, TextureParam.WrapT,
                (int) TextureParam.Repeat);

            GLInterops.TexParameterF(TextureParam.Texture2D, TextureParam.MinFilter,
                TextureParam.Linear);
            GLInterops.TexParameterF(TextureParam.Texture2D, TextureParam.MagFilter,
                TextureParam.Linear);

            GLInterops.TexImage2D(TextureParam.Texture2D, ColorFormat.Rgba, Width, Height,
                ColorFormat.Bgra, DataTypes.UByte, data.Scan0);

            GLInterops.BindTexture(TextureParam.Texture2D, 0);
            bitmap.UnlockBits(data);
            bitmap.Dispose();
        }

        /// <summary>
        /// Initializes a new GLTexture class
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="texture">The texture id</param>
        internal GLTexture(int width, int height, uint texture)
        {
            Width = width;
            Height = height;
            Id = texture;
        }

        /// <summary>
        /// Initializes a new GLTexture class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        internal GLTexture(Stream stream) : this((Bitmap) Image.FromStream(stream))
        {
        }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        internal uint Id { get; }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; }

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
            GLInterops.GetTexImage(TextureParam.Texture2D, ColorFormat.Rgba,
                DataTypes.UByte, _lockedData);
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
                GLInterops.TexSubImage2D(TextureParam.Texture2D, 0, (int) colordata.Position.X,
                    (int) colordata.Position.Y, 1, 1, ColorFormat.Rgba,
                    DataTypes.UByte, pixelData);
            }
            Unbind();

            IsLocked = false;
        }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the OpenGLTexture class.
        /// </summary>
        ~GLTexture()
        {
            Dispose(false);
        }

        /// <summary>
        /// Binds the current texture.
        /// </summary>
        internal void Bind()
        {
            GLInterops.ActiveTexture(TextureParam.Texture0);
            GLInterops.BindTexture(TextureParam.Texture2D, Id);
        }

        /// <summary>
        /// Unbinds the texture.
        /// </summary>
        internal void Unbind()
        {
            GLInterops.BindTexture(TextureParam.Texture2D, 0);
        }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lockedColors.Clear();
                _lockedData = null;
            }

            //Anything in opengl gets cleaned up if we destroy the context.
        }
    }
}
