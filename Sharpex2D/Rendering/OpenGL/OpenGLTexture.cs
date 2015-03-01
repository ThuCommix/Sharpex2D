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

using System.Drawing;
using System.Drawing.Imaging;


namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [MetaData("Name", "OpenGL Texture")]
    internal class OpenGLTexture : ITexture
    {
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
        /// Initializes a new OpenGLTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal OpenGLTexture(Bitmap bitmap)
        {
            Height = bitmap.Height;
            Width = bitmap.Width;
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var textureBuffer = new uint[1];
            OpenGLInterops.glGenTextures(1, textureBuffer);
            Id = textureBuffer[0];

            OpenGLInterops.glBindTexture(OpenGLInterops.GL_TEXTURE_2D, Id);
            OpenGLInterops.glTexParameteri(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_WRAP_S,
                (int) OpenGLInterops.GL_REPEAT);
            OpenGLInterops.glTexParameteri(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_WRAP_T,
                (int) OpenGLInterops.GL_REPEAT);

            OpenGLInterops.glTexParameterf(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_MIN_FILTER,
                SGL.SpriteBatch.InterpolationMode == InterpolationMode.Linear ? OpenGLInterops.GL_LINEAR : OpenGLInterops.GL_NEAREST);
            OpenGLInterops.glTexParameterf(OpenGLInterops.GL_TEXTURE_2D, OpenGLInterops.GL_TEXTURE_MAG_FILTER,
                SGL.SpriteBatch.InterpolationMode == InterpolationMode.Linear ? OpenGLInterops.GL_LINEAR : OpenGLInterops.GL_NEAREST);

            OpenGLInterops.glTexImage2D(OpenGLInterops.GL_TEXTURE_2D, 0, OpenGLInterops.GL_RGBA, Width, Height, 0,
                OpenGLInterops.GL_BGRA, OpenGLInterops.GL_UNSIGNED_BYTE, data.Scan0);

            OpenGLInterops.glBindTexture(OpenGLInterops.GL_TEXTURE_2D, 0);
            bitmap.UnlockBits(data);
            bitmap.Dispose();
        }

        /// <summary>
        /// Binds the current texture.
        /// </summary>
        internal void Bind()
        {
            OpenGLInterops.ActiveTexture(OpenGLInterops.GL_TEXTURE0);
            OpenGLInterops.glBindTexture(OpenGLInterops.GL_TEXTURE_2D, Id);
        }

        /// <summary>
        /// Unbinds the texture.
        /// </summary>
        internal void Unbind()
        {
            OpenGLInterops.glBindTexture(OpenGLInterops.GL_TEXTURE_2D, 0);
        }
    }
}
