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
using System.Collections.Generic;
using System.Windows.Forms;
using Sharpex2D.Surface;

namespace Sharpex2D.Input.XPlatform
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Keyboard : NativeInput<KeyboardState>
    {
        private readonly Dictionary<Keys, bool> _keystate;

        /// <summary>
        /// Initializes a new FluentKeyboard class.
        /// </summary>
        public Keyboard()
            : base(new Guid("55DDC560-40B5-487F-A47B-A265707E495D"))
        {
            IntPtr surfaceHandle = SGL.Components.Get<RenderTarget>().Handle;
            var surface = (Form) Control.FromHandle(surfaceHandle);
            _keystate = new Dictionary<Keys, bool>();
            surface.KeyDown += _surface_KeyDown;
            surface.KeyUp += _surface_KeyUp;
        }

        /// <summary>
        /// Gets the PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(5, 1); }
        }

        #region IUpdateable Implementation

        /// <summary>
        /// Called if the component should get updated.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _keystate.Clear();
        }

        #endregion

        /// <summary>
        /// Initializes the device.
        /// </summary>
        public override void InitializeDevice()
        {
        }

        /// <summary>
        /// Gets the State.
        /// </summary>
        /// <returns>KeyState.</returns>
        public override KeyboardState GetState()
        {
            return new KeyboardState(_keystate);
        }

        /// <summary>
        /// The KeyUp Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void _surface_KeyUp(object sender, KeyEventArgs e)
        {
            SetKeyState((Keys) e.KeyCode, false);

            if (e.Modifiers == System.Windows.Forms.Keys.None)
            {
                SetKeyState(Keys.Alt, false);
                SetKeyState(Keys.Control, false);
                SetKeyState(Keys.Shift, false);
            }
        }

        /// <summary>
        /// The KeyDown Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void _surface_KeyDown(object sender, KeyEventArgs e)
        {
            SetKeyState((Keys) e.KeyCode, true);
            SetKeyState((Keys) e.Modifiers, true);
        }

        /// <summary>
        /// Sets the internal KeyState.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <param name="state">The State.</param>
        private void SetKeyState(Keys key, bool state)
        {
            if (!_keystate.ContainsKey(key))
            {
                _keystate.Add(key, state);
            }
            _keystate[key] = state;
        }
    }
}