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

namespace Sharpex2D.Framework.Rendering
{
    public class GraphicsDevice : IComponent, IDisposable
    {
        /// <summary>
        /// Initializes a new GraphicsDeivce.
        /// </summary>
        /// <param name="gameWindow">The GameWindow.</param>
        /// <param name="graphicsManager">The GraphicsManager.</param>
        public GraphicsDevice(GameWindow gameWindow, GraphicsManager graphicsManager)
        {
            GameWindow = gameWindow;
            GraphicsManager = graphicsManager;
        }

        /// <summary>
        /// Determines if the graphics device is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Sets or gets the GameWindow.
        /// </summary>
        public GameWindow GameWindow { get; }

        /// <summary>
        /// Gets the graphics manager.
        /// </summary>
        public GraphicsManager GraphicsManager { get; }

        /// <summary>
        /// Gets the ScaleValue.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                Control control = Control.FromHandle(GameWindow.Handle);
                if (control == null)
                {
                    return new Vector2(1, 1);
                }

                float x = control.ClientSize.Width/(float) GraphicsManager.PreferredBackBufferWidth;
                float y = control.ClientSize.Height/(float) GraphicsManager.PreferredBackBufferHeight;

                return new Vector2(x, y);
            }
        }

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
                    GameWindow.Dispose();
                }
            }

            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
