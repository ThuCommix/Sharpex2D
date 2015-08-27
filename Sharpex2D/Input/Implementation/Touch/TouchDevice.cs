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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sharpex2D.Framework.Logging;

namespace Sharpex2D.Framework.Input.Implementation.Touch
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    internal class TouchDevice : ITouchInput
    {
        private readonly Logger _logger;
        private readonly List<Input.Touch> _touches;
        private IntPtr _handle;

        /// <summary>
        /// Initializes a new Touch class.
        /// </summary>
        public TouchDevice()
        {
            _touches = new List<Input.Touch>();
            _logger = LogManager.GetClassLogger();
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            _touches.Clear();
        }

        /// <summary>
        /// Initializes the input.
        /// </summary>
        public void Initialize()
        {
            IntPtr handle = GameHost.Get<GameWindow>().Handle;


            if (!TouchInterops.RegisterTouchWindow(handle, 0))
            {
                throw new InvalidOperationException("Unable to register TouchWindow.");
            }

            IsAvailable = true;

            var msgFilter = new MessageFilter(handle) {Filter = TouchInterops.WM_TOUCH};
            msgFilter.MessageArrived += MessageArrived;
            _handle = handle;
        }

        /// <summary>
        /// A value indicating whether the Joystick is available.
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Gets the State.
        /// </summary>
        /// <returns>TouchState.</returns>
        public TouchState GetState()
        {
            return new TouchState(_touches.ToArray());
        }

        /// <summary>
        /// MessageArrived event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void MessageArrived(object sender, MessageEventArgs e)
        {
            DecodeMessage(e.Message);
        }

        /// <summary>
        /// Decoces the message.
        /// </summary>
        /// <param name="m">The Message.</param>
        private void DecodeMessage(Message m)
        {
            int inputCount = m.WParam.ToInt32();

            var touchInput = new TouchInput[inputCount];

            if (!TouchInterops.GetTouchInputInfo(m.LParam, inputCount, touchInput, Marshal.SizeOf(touchInput)))
            {
                _logger.Warn("Error while extracting TouchInputInfo.");
                return;
            }

            for (int i = 0; i < inputCount; i++)
            {
                TouchInput touchInfo = touchInput[i];
                var touchMode = TouchMode.Down;

                if ((touchInfo.dwFlags & (int) TouchFlags.TOUCHEVENTF_DOWN) != 0)
                {
                    touchMode = TouchMode.Down;
                }
                else if ((touchInfo.dwFlags & (int) TouchFlags.TOUCHEVENTF_UP) != 0)
                {
                    touchMode = TouchMode.Up;
                }
                else if ((touchInfo.dwFlags & (int) TouchFlags.TOUCHEVENTF_MOVE) != 0)
                {
                    touchMode = TouchMode.Move;
                }


                var touch = new Input.Touch(touchInfo.dwID,
                    new Vector2(touchInfo.cxContact/100f, touchInfo.cyContact/100f),
                    new Vector2(touchInfo.x/100f, touchInfo.y/100f), new DateTime(0, 0, 0, 0, 0, 0, touchInfo.dwTime),
                    touchMode);

                _touches.Add(touch);


                if (!TouchInterops.CloseTouchInputHandle(m.LParam))
                {
                    _logger.Warn("Unable to close TouchInputHandle.");
                }
            }
        }

        /// <summary>
        /// Deconstructs the object.
        /// </summary>
        ~TouchDevice()
        {
            if (!TouchInterops.UnregisterTouchWindow(_handle))
            {
                _logger.Warn("Unable to unregister TouchWindow.");
            }
        }
    }
}