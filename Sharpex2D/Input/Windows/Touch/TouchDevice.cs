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
using Sharpex2D.Debug.Logging;
using Sharpex2D.Math;
using Sharpex2D.Surface;

namespace Sharpex2D.Input.Windows.Touch
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class TouchDevice : NativeInput<TouchState>
    {
        private readonly bool _apiSuccess;
        private readonly IntPtr _handle;
        private readonly Logger _logger;
        private readonly List<Input.Touch> _touches;

        /// <summary>
        ///     Initializes a new Touch class.
        /// </summary>
        public TouchDevice()
            : base(new Guid("0F29FED4-24B0-4D39-91FA-80D29388853B"))
        {
            IntPtr handle = SGL.Components.Get<RenderTarget>().Handle;

            try
            {
                if (!NativeMethods.RegisterTouchWindow(handle, 0))
                {
                    throw new InvalidOperationException("Unable to register TouchWindow.");
                }
                _apiSuccess = true;
            }
            catch (Exception)
            {
                _apiSuccess = false;
                return;
            }

            var msgFilter = new MessageFilter(handle) {Filter = NativeMethods.WM_TOUCH};
            msgFilter.MessageArrived += MessageArrived;
            _handle = handle;

            _touches = new List<Input.Touch>();

            _logger = LogManager.GetClassLogger();
        }

        /// <summary>
        ///     Gets the PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(6, 1); }
        }

        /// <summary>
        ///     A value indicating whether the Platform is supported.
        /// </summary>
        public override bool IsPlatformSupported
        {
            get { return _apiSuccess; }
        }

        /// <summary>
        ///     Initializes the device.
        /// </summary>
        public override void InitializeDevice()
        {
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _touches.Clear();
        }

        /// <summary>
        ///     Gets the State.
        /// </summary>
        /// <returns>TouchState.</returns>
        public override TouchState GetState()
        {
            return new TouchState(_touches.ToArray());
        }

        /// <summary>
        ///     MessageArrived event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void MessageArrived(object sender, MessageEventArgs e)
        {
            DecodeMessage(e.Message);
        }

        /// <summary>
        ///     Decoces the message.
        /// </summary>
        /// <param name="m">The Message.</param>
        private void DecodeMessage(Message m)
        {
            int inputCount = m.WParam.ToInt32();

            var touchInput = new TouchInput[inputCount];

            if (!NativeMethods.GetTouchInputInfo(m.LParam, inputCount, touchInput, Marshal.SizeOf(touchInput)))
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


                if (!NativeMethods.CloseTouchInputHandle(m.LParam))
                {
                    _logger.Warn("Unable to close TouchInputHandle.");
                }
            }
        }

        /// <summary>
        ///     Deconstructs the object.
        /// </summary>
        ~TouchDevice()
        {
            if (!NativeMethods.UnregisterTouchWindow(_handle))
            {
                _logger.Warn("Unable to unregister TouchWindow.");
            }
        }
    }

#endif
}