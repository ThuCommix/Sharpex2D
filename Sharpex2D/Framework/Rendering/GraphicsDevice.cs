using System;
using System.Windows.Forms;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D.Framework.Rendering
{
    public class GraphicsDevice : IComponent, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("9360F0CF-F712-499D-912A-891B1E35876C"); }
        }

        #endregion

        /// <summary>
        /// Determines if the graphics device is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get;
            private set;
        }
        /// <summary>
        /// Sets or gets the graphic resolution.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get;
            set;
        }
        /// <summary>
        /// Sets or gets the RenderTarget.
        /// </summary>
        public RenderTarget RenderTarget
        {
            get;
            internal set;
        }
        /// <summary>
        /// Initializes a new GraphicsDeivce.
        /// </summary>
        /// <param name="renderTarget">The RenderTarget.</param>
        public GraphicsDevice(RenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        /// <summary>
        /// Gets the ScaleValue.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                var control = Control.FromHandle(RenderTarget.Handle);
                if (control == null)
                {
                    return new Vector2(1, 1);
                }

                var x = control.ClientSize.Width / (float)DisplayMode.Width;
                var y = control.ClientSize.Height / (float)DisplayMode.Height;

                return new Vector2(x, y);
            }
        }

        /// <summary>
        /// Gets or sets the Clear Color.
        /// </summary>
        public Color ClearColor { set; get; }

        /// <summary>
        /// Gets the RefreshRate.
        /// </summary>
        public float RefreshRate { internal set; get; }

        /// <summary>
        /// Disposes the GraphicsDevice.
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
            RenderTarget = null;
        }
    }
}
