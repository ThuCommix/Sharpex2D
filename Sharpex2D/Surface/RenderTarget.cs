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
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Sharpex2D.Surface
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class RenderTarget : IComponent, IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new RenderTarget class.
        /// </summary>
        /// <param name="handle">The WindowHandle.</param>
        internal RenderTarget(IntPtr handle)
        {
            Handle = handle;
            Window = new GameWindow(handle);
            Window.FullscreenChanged += WindowFullscreenChanged;
            Window.ScreenSizeChanged += WindowScreenSizeChanged;
        }

        /// <summary>
        /// Gets the WindowHandle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Gets the ISurfaceControl.
        /// </summary>
        public GameWindow Window { private set; get; }

        /// <summary>
        /// A value indicating whether the surface is running in fullscreen.
        /// </summary>
        public bool IsFullscreen
        {
            get { return Window.IsFullscreen; }
        }

        /// <summary>
        /// A value indicating whether the RenderTarget is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
#if Windows
                return NativeMethods.IsWindow(Handle);
#elif Mono
				return Control.FromHandle(Handle) is Form;
				#endif
            }
        }

        /// <summary>
        /// Gets the RenderTarget associated with the current process.
        /// </summary>
        public static RenderTarget Default
        {
            get
            {
                IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

#if Windows
                if (NativeMethods.IsWindow(handle))
                {
                    return new RenderTarget(handle);
                }
#elif Mono
				if(Control.FromHandle(handle) is Form)
				{
					return new RenderTarget(handle);
				}
				#endif

                throw new InvalidOperationException("Could not get the handle associated with the current process.");
            }
        }

        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0F73D6D0-7CE8-4A77-A184-BE93E77E86B5"); }
        }

        #endregion

        /// <summary>
        /// ScreenSizeChanged event.
        /// </summary>
        public event ScreenSizeEventHandler ScreenSizeChanged;

        /// <summary>
        /// FullscreenChanged event.
        /// </summary>
        public event ScreenSizeEventHandler FullscreenChanged;

        /// <summary>
        /// WindowScreenChanged event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void WindowScreenSizeChanged(object sender, EventArgs e)
        {
            if (ScreenSizeChanged != null)
            {
                ScreenSizeChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// WindowFullscreenChanged event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void WindowFullscreenChanged(object sender, EventArgs e)
        {
            if (FullscreenChanged != null)
            {
                FullscreenChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Create a new RenderTarget from a specified handle.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        /// <returns>RenderTarget</returns>
        public static RenderTarget FromHandle(IntPtr handle)
        {
#if Windows
            if (NativeMethods.IsWindow(handle))
            {
                return new RenderTarget(handle);
            }
#elif Mono
			if(Control.FromHandle(handle) is Form)
			{
				return new RenderTarget(handle);
			}
			#endif

            throw new InvalidOperationException("The given Handle is not a window.");
        }

        /// <summary>
        /// Creates a new RenderTarget.
        /// </summary>
        /// <returns>RenderTarget</returns>
        public static RenderTarget Create()
        {
            var surface = new Form();

            new Thread(() => Application.Run(surface)).Start();

            while (!surface.IsHandleCreated)
            {
            }

            IntPtr handle = IntPtr.Zero;

            MethodInvoker br = delegate { handle = surface.Handle; };
            surface.Invoke(br);

            return new RenderTarget(handle);
        }

        #region IDisposable Implementation

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
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    Window.Dispose();
                }
            }
        }

        #endregion
    }
}