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
using System.Collections.Generic;
using System.Windows.Forms;
using Sharpex2D.Math;
using Sharpex2D.Surface;

namespace Sharpex2D.Input.Implementation
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class Mouse : IMouse
    {
        private readonly object _locker;
        private readonly Dictionary<MouseButtons, bool> _mousestate;
        private Vector2 _position;

        /// <summary>
        /// Initializes a new Mouse class.
        /// </summary>
        public Mouse()
        {
            IntPtr handle = SGL.Components.Get<RenderTarget>().Handle;
            _position = new Vector2(0, 0);
            Control control = Control.FromHandle(handle);
            _mousestate = new Dictionary<MouseButtons, bool>();
            _locker = new object();
            control.MouseMove += MouseMove;
            control.MouseDown += MouseDown;
            control.MouseUp += MouseUp;
            Handle = handle;
        }

        /// <summary>
        /// Represents the surface handle.
        /// </summary>
        public IntPtr Handle { private set; get; }


        /// <summary>
        /// Gets the State.
        /// </summary>
        /// <returns>MouseState.</returns>
        public MouseState GetState()
        {
            lock (_locker)
            {
                return new MouseState(_mousestate, _position);
            }
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Initializes the input.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Sets the internal button state.
        /// </summary>
        /// <param name="button">The Button.</param>
        /// <param name="state">The State.</param>
        private void SetButtonState(MouseButtons button, bool state)
        {
            lock (_locker)
            {
                if (!_mousestate.ContainsKey(button))
                {
                    _mousestate.Add(button, state);
                }
                _mousestate[button] = state;
            }
        }

        /// <summary>
        /// Triggered if the mouse buttons are pressed up.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void MouseUp(object sender, MouseEventArgs e)
        {
            SetButtonState((MouseButtons) e.Button, false);
        }

        /// <summary>
        /// Triggered if the mouse buttons are pressed down.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void MouseDown(object sender, MouseEventArgs e)
        {
            SetButtonState((MouseButtons) e.Button, true);
        }

        /// <summary>
        /// Triggered if the mouse moved.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void MouseMove(object sender, MouseEventArgs e)
        {
            _position = new Vector2(e.Location.X/SGL.GraphicsDevice.Scale.X, e.Location.Y/SGL.GraphicsDevice.Scale.Y);
        }
    }
}