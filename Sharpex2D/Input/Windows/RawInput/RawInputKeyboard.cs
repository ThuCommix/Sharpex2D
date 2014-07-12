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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sharpex2D.Surface;

namespace Sharpex2D.Input.Windows.RawInput
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class RawInputKeyboard : InputDevice<KeyboardState>, INativeKeyboard
    {
        private readonly Dictionary<Keys, bool> _keystate;
        private readonly MessageFilter _msgFilter;
        private bool _initialized;
        private InputData _rawBuffer;

        /// <summary>
        ///     Initializes a new Keyboard class.
        /// </summary>
        public RawInputKeyboard()
            : base(new Guid("{827690DE-FE6C-46B4-9FE0-9A7A53AB9F99}"))
        {
            IntPtr handle = SGL.Components.Get<RenderTarget>().Handle;
            _msgFilter = new MessageFilter(handle) {Filter = 0x00FF};
            _msgFilter.MessageArrived += MessageArrived;

            _keystate = new Dictionary<Keys, bool>();
        }

        /// <summary>
        ///     Gets the PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(5, 1); }
        }

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        public override void InitializeDevice()
        {
            if (!_initialized)
            {
                _initialized = true;

                var rid = new RawInputDevice[1];

                rid[0].UsagePage = HidUsagePage.GENERIC;
                rid[0].Usage = HidUsage.Keyboard;
                rid[0].Flags = RawInputDeviceFlags.INPUTSINK;
                rid[0].Target = _msgFilter.Handle;

                if (!NativeMethods.RegisterRawInputDevices(rid, (uint) rid.Length, (uint) Marshal.SizeOf(rid[0])))
                {
                    throw new ApplicationException("Failed to register raw input device.");
                }
            }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _keystate.Clear();
        }

        /// <summary>
        ///     Gets the State.
        /// </summary>
        /// <returns>KeyState.</returns>
        public override KeyboardState GetState()
        {
            return new KeyboardState(_keystate);
        }

        /// <summary>
        ///     Processes the messages.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void MessageArrived(object sender, MessageEventArgs e)
        {
            Message m = e.Message;
            int dwSize = 0;
            NativeMethods.GetRawInputData(m.LParam, DataCommand.RID_INPUT, IntPtr.Zero, ref dwSize,
                Marshal.SizeOf(typeof (RawInputHeader)));

            if (dwSize !=
                NativeMethods.GetRawInputData(m.LParam, DataCommand.RID_INPUT, out _rawBuffer, ref dwSize,
                    Marshal.SizeOf(typeof (RawInputHeader))))
            {
                System.Diagnostics.Debug.WriteLine("Error getting the rawinput buffer.");
            }

            int virtualKey = _rawBuffer.data.keyboard.VKey;
            int flags = _rawBuffer.data.keyboard.Flags;

            bool isBreak = ((flags & 0x01) != 0); //isBreak = keydown

            SetKeyState((Keys) virtualKey, !isBreak);
        }

        /// <summary>
        ///     Sets the internal KeyState.
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

#endif
}