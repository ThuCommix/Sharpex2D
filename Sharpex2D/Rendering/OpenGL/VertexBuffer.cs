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
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class VertexBuffer : IDisposable
    {
        /// <summary>
        /// Initializes a new VertexBuffer class.
        /// </summary>
        public VertexBuffer()
        {
            var buffers = new uint[1];
            OpenGLInterops.GenBuffers(1, buffers);
            if (buffers[0] == 0) throw new GraphicsException("Unable to allocate memory for index buffer.");

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
        /// Deconstructs the VertexBuffer class.
        /// </summary>
        ~VertexBuffer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Binds the VertexBuffer.
        /// </summary>
        public void Bind()
        {
            OpenGLInterops.BindBuffer(BufferTarget.ArrayBuffer, Id);
        }

        /// <summary>
        /// Sets the Data.
        /// </summary>
        /// <param name="vertices">The Vertices.</param>
        /// <remarks>Bind must be called in order to take effect.</remarks>
        public void SetData(float[] vertices)
        {
            OpenGLInterops.BufferData(BufferTarget.ArrayBuffer, vertices, DrawMode.StaticDraw);
        }

        /// <summary>
        /// Unbinds the VertexBuffer.
        /// </summary>
        public void Unbind()
        {
            OpenGLInterops.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            try
            {
                OpenGLInterops.DeleteBuffers(1, new[] {Id});
            }
            catch
            {
                Logger.Instance.Warn("Unable to dispose.");
            }
        }

        /// <summary>
        /// Enables the vertex attribute array.
        /// </summary>
        /// <param name="index">The Index.</param>
        public static void EnableVertexAttribArray(uint index)
        {
            OpenGLInterops.EnableVertexAttribArray(index);
        }

        /// <summary>
        /// Pushs the vertex attributes description.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="size">The Size.</param>
        /// <param name="normalized">The normalized State.</param>
        /// <param name="stride">The Stride.</param>
        /// <param name="offset">The Offset.</param>
        public static void VertexAttribPointer(uint index, int size, bool normalized, int stride, int offset)
        {
            OpenGLInterops.VertexAttribPointer(index, size, DataTypes.Float, normalized, stride,
                new IntPtr(offset));
        }
    }
}