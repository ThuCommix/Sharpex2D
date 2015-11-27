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
using SharpDX.Direct3D9;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    public class DirectXRenderTarget2D : IRenderTarget2D
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
        /// Gets the surface
        /// </summary>
        public Surface Surface { get; }

        private readonly DirectXTexture _texture;

        /// <summary>
        /// Initializes a new DirectXRenderTarget2D class
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public DirectXRenderTarget2D(int width, int height)
        {
            Width = width;
            Height = height;

            _texture = new DirectXTexture(width, height);
            Surface = _texture.InternalTexture.GetSurfaceLevel(0);
        }

        /// <summary>
        /// Deconstructs the DirectXRenderTarget2D class
        /// </summary>
        ~DirectXRenderTarget2D()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the texture
        /// </summary>
        /// <returns>Returns the texture from the render target</returns>
        public ITexture GetTexture()
        {
            return _texture;
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
            if (disposing)
            {
                _texture.Dispose();
                Surface.Dispose();
            }
        }
    }
}
