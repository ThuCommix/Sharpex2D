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
using System.Linq;
using System.Runtime.InteropServices;

namespace Sharpex2D.Rendering.OpenGL.Windows
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class NativeRenderContext : IRenderContext
    {
        private IntPtr _deviceContext;
        private IntPtr _renderContext;
        private IntPtr _windowHandle;

        /// <summary>
        /// Initializes a new NativeRenderContext class.
        /// </summary>
        public NativeRenderContext() : this(OpenGLVersion.Compatibility)
        {
        }

        /// <summary>
        /// Initializes a new NativeRenderContext class.
        /// </summary>
        /// <param name="openglVersion">The OpenGLVersion.</param>
        public NativeRenderContext(OpenGLVersion openglVersion)
        {
            CurrentVersion = openglVersion;
        }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { private set; get; }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { private set; get; }

        /// <summary>
        /// Gets the current OpenGLVersion.
        /// </summary>
        public OpenGLVersion CurrentVersion { get; private set; }

        /// <summary>
        /// Makes the render context the current context.
        /// </summary>
        public void MakeCurrent()
        {
            if (_renderContext != IntPtr.Zero)
            {
                NativeMethods.wglMakeCurrent(_deviceContext, _renderContext);
            }
        }

        /// <summary>
        /// Creates a new RenderContext.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="colorDepth">The ColorDepth.</param>
        /// <param name="parameter">The optional parameter.</param>
        public void Create(int width, int height, int colorDepth, object parameter = null)
        {
            if (parameter is IntPtr)
            {
                Create(width, height, colorDepth, (IntPtr) parameter);
            }
            else
            {
                throw new GraphicsException("An invalid parameter was passed to the RenderContext.");
            }
        }

        /// <summary>
        /// Blit the rendered data to the supplied device context.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        public void Blit(IntPtr hdc)
        {
            if (_deviceContext != IntPtr.Zero || _windowHandle != IntPtr.Zero)
            {
                NativeMethods.SwapBuffers(_deviceContext);
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
        /// Creates a NativeRenderContext.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="bitDepth">The BitDepth.</param>
        /// <param name="windowHandle">The WindowHandle.</param>
        private void Create(int width, int height, int bitDepth, IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
            Width = width;
            Height = height;

            _deviceContext = NativeMethods.GetDC(windowHandle);

            var pfd = new PixelFormatDescriptor(); // The pixel format descriptor
            pfd.nSize = (short) Marshal.SizeOf(pfd); // Size of the pixel format descriptor
            pfd.nVersion = 1; // Version number (always 1)
            pfd.dwFlags = PixelFormatDescription.PFD_DRAW_TO_WINDOW | // Format must support windowed mode
                          PixelFormatDescription.PFD_SUPPORT_OPENGL | // Format must support OpenGL
                          PixelFormatDescription.PFD_DOUBLEBUFFER; // Must support double buffering
            pfd.iPixelType = PixelFormatType.PFD_TYPE_RGBA; // Request an RGBA format
            pfd.cColorBits = (byte) bitDepth; // Select our color depth
            pfd.cRedBits = 0; // Individual color bits ignored
            pfd.cRedShift = 0;
            pfd.cGreenBits = 0;
            pfd.cGreenShift = 0;
            pfd.cBlueBits = 0;
            pfd.cBlueShift = 0;
            pfd.cAlphaBits = 0; // No alpha buffer
            pfd.cAlphaShift = 0; // Alpha shift bit ignored
            pfd.cAccumBits = 0; // Accumulation buffer
            pfd.cAccumRedBits = 0; // Individual accumulation bits ignored
            pfd.cAccumGreenBits = 0;
            pfd.cAccumBlueBits = 0;
            pfd.cAccumAlphaBits = 0;
            pfd.cDepthBits = 24; // Z-buffer (depth buffer)
            pfd.cStencilBits = 8; // No stencil buffer
            pfd.cAuxBuffers = 0; // No auxiliary buffer
            pfd.iLayerType = LayerType.PFD_MAIN_PLANE; // Main drawing layer
            pfd.bReserved = 0; // Reserved
            pfd.dwLayerMask = 0; // Layer masks ignored
            pfd.dwVisibleMask = 0;
            pfd.dwDamageMask = 0;

            int iPixelFormat;
            if ((iPixelFormat = NativeMethods.ChoosePixelFormat(_deviceContext, ref pfd)) == 0)
            {
                throw new GraphicsException("Unable to initialize NativeRenderContext.");
            }

            if (!NativeMethods.SetPixelFormat(_deviceContext, iPixelFormat, ref pfd))
            {
                throw new GraphicsException("Unable to initialize NativeRenderContext.");
            }

            _renderContext = NativeMethods.wglCreateContext(_deviceContext);
            MakeCurrent();

            if (CurrentVersion != OpenGLVersion.Compatibility)
                CreateWithVersionHint();
        }

        /// <summary>
        /// Creates the context with the specified OpenGLVersion.
        /// </summary>
        private void CreateWithVersionHint()
        {
            VersionAttribute versionAttrib =
                CurrentVersion.GetType()
                    .GetMember(CurrentVersion.ToString())
                    .Single()
                    .GetCustomAttributes(typeof (VersionAttribute), false)
                    .OfType<VersionAttribute>()
                    .FirstOrDefault();
            if (versionAttrib == null)
                throw new InvalidOperationException("Unable to read the version attribute.");

            if (versionAttrib.Major >= 3)
            {
                try
                {
                    int[] attributes =
                    {
                        OpenGL.WGL_CONTEXT_MAJOR_VERSION_ARB, versionAttrib.Major,
                        OpenGL.WGL_CONTEXT_MINOR_VERSION_ARB, versionAttrib.Minor,
                        OpenGL.WGL_CONTEXT_FLAGS_ARB, OpenGL.WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB,
                        0
                    };
                    IntPtr hrc = OpenGL.CreateContextAttribsARB(_deviceContext, IntPtr.Zero, attributes);
                    NativeMethods.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                    NativeMethods.wglDeleteContext(_renderContext);
                    NativeMethods.wglMakeCurrent(_deviceContext, hrc);
                    _renderContext = hrc;
                }
                catch
                {
                    throw new InvalidOperationException(string.Format("Unable to create {0}.{1} context.",
                        versionAttrib.Major, versionAttrib.Minor));
                }
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                NativeMethods.ReleaseDC(_windowHandle, _deviceContext);

                if (_renderContext != IntPtr.Zero)
                {
                    NativeMethods.wglDeleteContext(_renderContext);
                    _renderContext = IntPtr.Zero;
                }
            }
        }
    }
#endif
}