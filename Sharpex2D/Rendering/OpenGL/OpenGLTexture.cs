// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Drawing;
using System.Drawing.Imaging;
using Sharpex2D.Content.Pipeline;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Content("OpenGL Texture")]
    public class OpenGLTexture : Texture2D
    {
        private readonly int _height;
        private readonly OpenGLGraphics _openglGraphics;
        private readonly int _width;

        /// <summary>
        /// Initializes a new OpenGLTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal OpenGLTexture(Bitmap bitmap)
        {
            _width = bitmap.Width;
            _height = bitmap.Height;
            RawBitmap = bitmap;

            var oglGraphics = SGL.SpriteBatch.Graphics as OpenGLGraphics;
            if (oglGraphics == null) throw new InvalidOperationException("OpenGLGraphics not present.");

            _openglGraphics = oglGraphics;

            BindIfUnbinded();
        }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public override int Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public override int Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Gets the TextureId.
        /// </summary>
        public int TextureId { private set; get; }

        /// <summary>
        /// Gets the RawBitmap.
        /// </summary>
        internal Bitmap RawBitmap { private set; get; }

        internal bool IsBinded { private set; get; }

        /// <summary>
        /// Binds the texture if unbinded.
        /// </summary>
        internal void BindIfUnbinded()
        {
            if (IsBinded) return;

            var texture = new uint[1];

            OpenGL.glHint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);

            OpenGL.glGenTextures(1, texture);
            OpenGLHelper.ThrowLastError();
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, texture[0]);
            TextureId = (int) texture[0];

            BitmapData data = RawBitmap.LockBits(new Rectangle(0, 0, RawBitmap.Width, RawBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            if (_openglGraphics.InterpolationMode == InterpolationMode.NearestNeighbor)
            {
                OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, (int) OpenGL.GL_NEAREST);
                OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, (int) OpenGL.GL_NEAREST);
            }
            else
            {
                OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, (int) OpenGL.GL_LINEAR);
                OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, (int) OpenGL.GL_LINEAR);
            }

            OpenGL.glTexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA8,
                _width, _height, 0, OpenGL.GL_BGRA,
                OpenGL.GL_UNSIGNED_BYTE, data.Scan0);

            RawBitmap.UnlockBits(data);

            IsBinded = true;
        }
    }
}