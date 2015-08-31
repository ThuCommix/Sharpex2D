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
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal class RenderContext
    {
        private IntPtr _deviceContext;
        private IntPtr _renderContext;
        private IntPtr _windowHandle;

        /// <summary>
        /// Destructs the RenderContext class.
        /// </summary>
        ~RenderContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes the RenderContext.
        /// </summary>
        public void Initialize()
        {
            _windowHandle = GameHost.Get<GameWindow>().Handle;

            _deviceContext = OpenGLInterops.GetDC(_windowHandle);

            var pfd = new PixelFormatDescriptor();
            pfd.Size = (short) Marshal.SizeOf(pfd);
            pfd.Version = 1;
            pfd.FormatDescription = PixelFormatDescription.DrawToWindow |
                                    PixelFormatDescription.SupportOpenGL |
                                    PixelFormatDescription.DoubleBuffer;
            pfd.PixelType = 0;
            pfd.ColorBits = 24;
            pfd.RedBits = 0;
            pfd.RedShift = 0;
            pfd.GreenBits = 0;
            pfd.GreenShift = 0;
            pfd.BlueBits = 0;
            pfd.BlueShift = 0;
            pfd.AlphaBits = 0;
            pfd.AlphaShift = 0;
            pfd.AccumBits = 0;
            pfd.AccumRedBits = 0;
            pfd.AccumGreenBits = 0;
            pfd.AccumBlueBits = 0;
            pfd.AccumAlphaBits = 0;
            pfd.DepthBits = 24;
            pfd.StencilBits = 8;
            pfd.AuxBuffers = 0;
            pfd.LayerType = 0;
            pfd.Reserved = 0;
            pfd.LayerMask = 0;
            pfd.VisibleMask = 0;
            pfd.DamageMask = 0;

            int pixelFormat;
            if ((pixelFormat = OpenGLInterops.ChoosePixelFormat(_deviceContext, ref pfd)) == 0)
            {
                throw new GraphicsException("Unable to choose pixel format.");
            }

            if (!OpenGLInterops.SetPixelFormat(_deviceContext, pixelFormat, ref pfd))
            {
                throw new GraphicsException("Unable to set pixel format.");
            }

            _renderContext = OpenGLInterops.wglCreateContext(_deviceContext);
            OpenGLInterops.wglMakeCurrent(_deviceContext, _renderContext);

            try
            {
                int[] attributes =
                {
                    (int) ContextAttributes.MajorVersion, 3,
                    (int) ContextAttributes.MinorVersion, 3,
                    (int) ContextAttributes.Flags, (int) ContextAttributes.ForwardCompatible,
                    0
                };

                IntPtr hrc = OpenGLInterops.CreateContextWithAttributes(_deviceContext, IntPtr.Zero, attributes);
                OpenGLInterops.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                OpenGLInterops.wglDeleteContext(_renderContext);
                OpenGLInterops.wglMakeCurrent(_deviceContext, hrc);
                _renderContext = hrc;
            }
            catch (MissingMethodException)
            {
                throw new GraphicsException("Unable to create OpenGL 3.3 context.");
            }
        }

        /// <summary>
        /// Makes this context the current context.
        /// </summary>
        public void MakeCurrent()
        {
            if (_renderContext != IntPtr.Zero)
            {
                OpenGLInterops.wglMakeCurrent(_deviceContext, _renderContext);
            }
        }

        /// <summary>
        /// Swaps the buffers.
        /// </summary>
        public void SwapBuffers()
        {
            if (_deviceContext != IntPtr.Zero || _windowHandle != IntPtr.Zero)
            {
                OpenGLInterops.SwapBuffers(_deviceContext);
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            OpenGLInterops.ReleaseDC(_windowHandle, _deviceContext);
            if (_renderContext != IntPtr.Zero)
            {
                OpenGLInterops.wglDeleteContext(_renderContext);
                _renderContext = IntPtr.Zero;
            }

            if (disposing)
            {
            }
        }
    }
}
