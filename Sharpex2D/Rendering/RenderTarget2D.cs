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

namespace Sharpex2D.Framework.Rendering
{
    public class RenderTarget2D : IDisposable
    {
        /// <summary>
        /// Gets the internal render target
        /// </summary>
        internal IRenderTarget2D Instance { get; }

        /// <summary>
        /// Gets the width
        /// </summary>
        public int Width => Instance.Width;

        /// <summary>
        /// Gets the height
        /// </summary>
        public int Height => Instance.Height;

        /// <summary>
        /// A value indicating whether the render target is read only
        /// </summary>
        public bool ReadOnly { internal set; get; }

        /// <summary>
        /// Initializes a new RenderTarget2D class
        /// </summary>
        /// <param name="renderTarget">The internal render target</param>
        internal RenderTarget2D(IRenderTarget2D renderTarget)
        {
            Instance = renderTarget;
        }

        /// <summary>
        /// Deconstructs the RenderTarget2D class
        /// </summary>
        ~RenderTarget2D()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the texture2d
        /// </summary>
        /// <returns>Returns the texture from the render target</returns>
        public Texture2D GetTexture()
        {
            if(ReadOnly)
                throw new GraphicsException("The render target is in readonly mode - please finish pending drawing calls.");

            return new Texture2D(Instance.GetTexture());
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
                Instance.Dispose();
            }
        }
    }
}
