using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Surface
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class RenderTarget : IComponent, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0F73D6D0-7CE8-4A77-A184-BE93E77E86B5"); }
        }

        #endregion

        private bool _isDisposed;

        #region IDisposable Implementation

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
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

        /// <summary>
        ///     Initializes a new RenderTarget class.
        /// </summary>
        /// <param name="handle">The WindowHandle.</param>
        internal RenderTarget(IntPtr handle)
        {
            Handle = handle;
            Window = new GameWindow(handle);
        }

        /// <summary>
        ///     Gets the WindowHandle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        ///     Gets the ISurfaceControl.
        /// </summary>
        public GameWindow Window { private set; get; }

        /// <summary>
        ///     A value indicating whether the surface is running in fullscreen.
        /// </summary>
        public bool IsFullscreen
        {
            get { return Window.IsFullscreen; }
        }

        /// <summary>
        ///     A value indicating whether the RenderTarget is valid.
        /// </summary>
        public bool IsValid
        {
            get { return NativeMethods.IsWindow(Handle); }
        }

        /// <summary>
        ///     Gets the RenderTarget associated with the current process.
        /// </summary>
        public static RenderTarget Default
        {
            get
            {
                IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

                if (NativeMethods.IsWindow(handle))
                {
                    return new RenderTarget(handle);
                }

                throw new InvalidOperationException("Could not get the handle associated with the current process.");
            }
        }

        /// <summary>
        ///     Create a new RenderTarget from a specified handle.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        /// <returns>RenderTarget</returns>
        public static RenderTarget FromHandle(IntPtr handle)
        {
            if (NativeMethods.IsWindow(handle))
            {
                return new RenderTarget(handle);
            }

            throw new InvalidOperationException("The given Handle is not a window.");
        }

        /// <summary>
        ///     Creates a new RenderTarget.
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
    }
}