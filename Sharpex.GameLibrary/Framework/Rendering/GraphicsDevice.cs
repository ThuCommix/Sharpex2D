using System;
using System.Windows.Forms;
using SharpexGL.Framework.Components;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering
{
    public class GraphicsDevice : IComponent, IDisposable
    {
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
        /// Sets or gets the DeviceHandle.
        /// </summary>
        public IntPtr DeviceHandle
        {
            get;
            internal set;
        }
        /// <summary>
        /// Initializes a new GraphicsDeivce.
        /// </summary>
        /// <param name="deviceHandle">The Handle.</param>
        public GraphicsDevice(IntPtr deviceHandle)
        {
            DeviceHandle = deviceHandle;
        }

        /// <summary>
        /// Gets the ScaleValue.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                var control = Control.FromHandle(DeviceHandle);
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
        /// Disposes the GraphicsDevice.
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
            DeviceHandle = IntPtr.Zero;
        }
    }
}
