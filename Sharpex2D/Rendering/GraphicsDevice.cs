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
using System.Windows.Forms;
using Sharpex2D.Math;
using Sharpex2D.Surface;

namespace Sharpex2D.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GraphicsDevice : IComponent, IDisposable
    {
        private Color _clearColor;

        /// <summary>
        /// Initializes a new GraphicsDeivce.
        /// </summary>
        /// <param name="renderTarget">The RenderTarget.</param>
        public GraphicsDevice(RenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        /// <summary>
        /// Determines if the graphics device is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Sets or gets the graphic resolution.
        /// </summary>
        public BackBuffer BackBuffer { get; internal set; }

        /// <summary>
        /// Sets or gets the RenderTarget.
        /// </summary>
        public RenderTarget RenderTarget { get; internal set; }

        /// <summary>
        /// Gets the ScaleValue.
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

                float x = control.ClientSize.Width/(float) BackBuffer.Width;
                float y = control.ClientSize.Height/(float) BackBuffer.Height;

                return new Vector2(x, y);
            }
        }

        /// <summary>
        /// Gets or sets the Clear Color.
        /// </summary>
        public Color ClearColor
        {
            set
            {
                _clearColor = value;
                if (ClearColorChanged != null)
                {
                    ClearColorChanged(this, EventArgs.Empty);
                }
            }
            get { return _clearColor; }
        }

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
        /// Disposes the GraphicsDevice.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the GraphicsDevice class.
        /// </summary>
        ~GraphicsDevice()
        {
            Dispose(false);
        }

        /// <summary>
        /// Triggered if the clear color changed.
        /// </summary>
        public event EventHandler<EventArgs> ClearColorChanged;


        /// <summary>
        /// Triggered if the graphics device is disposed.
        /// </summary>
        public event EventHandler<EventArgs> Disposed;

        /// <summary>
        /// Disposes the object.
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

            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }
    }
}