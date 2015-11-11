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
    internal class VertexArray : IDisposable
    {
        /// <summary>
        /// Initializes a new VertexArray class.
        /// </summary>
        public VertexArray()
        {
            var buffers = new uint[1];
            GLInterops.GenVertexArrays(1, buffers);
            if (buffers[0] == 0) throw new GraphicsException("Unable to allocate memory for vertex array.");

            Id = buffers[0];
        }

        /// <summary>
        /// Gets the opengl identifer.
        /// </summary>
        public uint Id { get; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the VertexArray class.
        /// </summary>
        ~VertexArray()
        {
            Dispose(false);
        }

        /// <summary>
        /// Binds the VertexArray.
        /// </summary>
        public void Bind()
        {
            GLInterops.BindVertexArray(Id);
        }

        /// <summary>
        /// Unbinds the VertexArray.
        /// </summary>
        public void Unbind()
        {
            GLInterops.BindVertexArray(0);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            try
            {
                GLInterops.DeleteVertexArrays(1, new[] {Id});
            }
            catch
            {
                Logger.Instance.Warn("Unable to dispose.");
            }
        }
    }
}
