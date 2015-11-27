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

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal class GLFrameBuffer : IRenderTarget2D
    {
        /// <summary>
        /// Gets the width
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the framebuffer id
        /// </summary>
        public uint FramebufferId { get; }

        /// <summary>
        /// Gets the renderbuffer id
        /// </summary>
        public uint RenderbufferId { get; }

        /// <summary>
        /// Gets the texture id
        /// </summary>
        public uint TextureId { get; }

        private readonly GLTexture _glTexture;

        /// <summary>
        /// Initializes a new GLFrameBuffer class
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public GLFrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            try
            {
                TextureId = GLInterops.GenTexture();
                GLInterops.BindTexture(TextureParam.Texture2D, TextureId);

                GLInterops.TexParameterI(TextureParam.Texture2D, TextureParam.WrapS,
                    (int) TextureParam.Repeat);
                GLInterops.TexParameterI(TextureParam.Texture2D, TextureParam.WrapT,
                    (int) TextureParam.Repeat);
                GLInterops.TexParameterF(TextureParam.Texture2D, TextureParam.MinFilter,
                    TextureParam.Linear);
                GLInterops.TexParameterF(TextureParam.Texture2D, TextureParam.MagFilter,
                    TextureParam.Linear);
                GLInterops.TexImage2D(TextureParam.Texture2D, ColorFormat.Rgba, width, height, ColorFormat.Bgra,
                    DataTypes.UByte, IntPtr.Zero);

                RenderbufferId = GLInterops.GenRenderbuffer();
                GLInterops.BindRenderbuffer(RenderbufferId);
                GLInterops.RenderbufferStorage(width, height);
                GLInterops.BindRenderbuffer(0);

                FramebufferId = GLInterops.GenFramebuffer();
                GLInterops.BindFramebuffer(FramebufferId);

                GLInterops.AttachRenderbuffer(RenderbufferId);
                GLInterops.AttachTexture2D(TextureId);
                GLInterops.DrawBuffer(GLInterops.GLColorAttachment0);

                if (!GLInterops.CheckFramebufferStatus(FramebufferId))
                    throw new InvalidOperationException();

                GLInterops.BindTexture(TextureParam.Texture2D, 0);
                GLInterops.BindFramebuffer(0);

                _glTexture = new GLTexture(width, height, TextureId);
            }
            catch (Exception)
            {
                throw new GraphicsException("Unable to create OpenGL framebuffer.");
            }
        }

        /// <summary>
        /// Deconstructs the GLFrameBuffer class
        /// </summary>
        ~GLFrameBuffer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the texture
        /// </summary>
        /// <returns>Returns the texture from the render target</returns>
        public ITexture GetTexture()
        {
            return _glTexture;
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected void Dispose(bool disposing)
        {
            try
            {
                GLInterops.DeleteFramebuffer(FramebufferId);
                GLInterops.DeleteTexture(TextureId);
                GLInterops.DeleteRenderbuffer(RenderbufferId);
            }
            catch (Exception)
            {
                Logger.Instance.Debug("Unable to dispose framebuffer");
            }
        }
    }
}
