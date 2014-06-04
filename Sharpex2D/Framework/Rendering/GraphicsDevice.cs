using System;
using System.Windows.Forms;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GraphicsDevice : IComponent, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("9360F0CF-F712-499D-912A-891B1E35876C"); }
        }

        #endregion

        /// <summary>
        ///     Initializes a new GraphicsDeivce.
        /// </summary>
        /// <param name="renderTarget">The RenderTarget.</param>
        public GraphicsDevice(RenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        /// <summary>
        ///     Determines if the graphics device is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Sets or gets the graphic resolution.
        /// </summary>
        public DisplayMode DisplayMode { get; set; }

        /// <summary>
        ///     Sets or gets the RenderTarget.
        /// </summary>
        public RenderTarget RenderTarget { get; internal set; }

        /// <summary>
        ///     Gets the ScaleValue.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                Control control = Control.FromHandle(RenderTarget.Handle);
                if (control == null)
                {
                    return new Vector2(1, 1);
                }

                float x = control.ClientSize.Width/(float) DisplayMode.Width;
                float y = control.ClientSize.Height/(float) DisplayMode.Height;

                return new Vector2(x, y);
            }
        }

        /// <summary>
        ///     Gets or sets the Clear Color.
        /// </summary>
        public Color ClearColor { set; get; }

        /// <summary>
        ///     Gets the RefreshRate.
        /// </summary>
        public float RefreshRate { internal set; get; }

        /// <summary>
        ///     Disposes the GraphicsDevice.
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
            if (!IsDisposed)
            {
                IsDisposed = true;
                if (disposing)
                {
                    RenderTarget.Dispose();
                }
            }
        }
    }
}