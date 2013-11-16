using System;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Surface
{
    public class RenderTarget : IComponent
    {
        /// <summary>
        /// Initializes a new RenderTarget class.
        /// </summary>
        /// <param name="handle">The WindowHandle.</param>
        public RenderTarget(IntPtr handle)
        {
            Handle = handle;
            SurfaceControl = new WindowController(this);
        }

        /// <summary>
        /// Gets the WindowHandle.
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// Gets the ISurfaceControl.
        /// </summary>
        public ISurfaceControl SurfaceControl { private set; get; }
    }
}
