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

using Sharpex2D.Input.Windows.XInput;
using Sharpex2D.Math;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class GamepadState : IInputState
    {
        private XInputState _state;

        /// <summary>
        ///     Initializes a new GamepadState class.
        /// </summary>
        /// <param name="inputState">The InputStat.</param>
        internal GamepadState(XInputState inputState)
        {
            _state = inputState;
        }

        /// <summary>
        ///     A value indicating whether the D-Pad up is pressed.
        /// </summary>
        public bool IsDPadUpPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_UP); }
        }

        /// <summary>
        ///     A value indicating whether the D-Pad down is pressed.
        /// </summary>
        public bool IsDPadDownPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN); }
        }

        /// <summary>
        ///     A value indicating whether the D-Pad left is pressed.
        /// </summary>
        public bool IsDPadLeftPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT); }
        }

        /// <summary>
        ///     A value indicating whether the D-Pad right is pressed.
        /// </summary>
        public bool IsDPadRightPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT); }
        }

        /// <summary>
        ///     A value indicating whether A is pressed.
        /// </summary>
        public bool IsAPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_A); }
        }

        /// <summary>
        ///     A value indicating whether B is pressed.
        /// </summary>
        public bool IsBPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_B); }
        }

        /// <summary>
        ///     A value indicating whether X is pressed.
        /// </summary>
        public bool IsXPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_X); }
        }

        /// <summary>
        ///     A value indicating whether Y is pressed.
        /// </summary>
        public bool IsYPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_Y); }
        }

        /// <summary>
        ///     A value indicating whether back is pressed.
        /// </summary>
        public bool IsBackPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_BACK); }
        }

        /// <summary>
        ///     A value indicating whether start is pressed.
        /// </summary>
        public bool IsStartPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_START); }
        }

        /// <summary>
        ///     A value indicating whether left-shoulder is pressed.
        /// </summary>
        public bool IsLeftShoulderPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER); }
        }

        /// <summary>
        ///     A value indicating whether right-shoulder is pressed.
        /// </summary>
        public bool IsRightShoulderPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER); }
        }

        /// <summary>
        ///     A value indicating whether left-stick is pressed.
        /// </summary>
        public bool IsLeftStickPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_LEFT_THUMB); }
        }

        /// <summary>
        ///     A value indicating whether right-stick is pressed.
        /// </summary>
        public bool IsRightStickPressed
        {
            get { return _state.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_RIGHT_THUMB); }
        }

        /// <summary>
        ///     A value indicating whether left-trigger is pressed.
        /// </summary>
        public int LeftTrigger
        {
            get { return _state.Gamepad.bLeftTrigger; }
        }

        /// <summary>
        ///     A value indicating whether right-trigger is pressed.
        /// </summary>
        public int RightTrigger
        {
            get { return _state.Gamepad.bRightTrigger; }
        }

        /// <summary>
        ///     Gets the LeftThumbStick.
        /// </summary>
        public Vector2 LeftThumbStick
        {
            get { return new Vector2(_state.Gamepad.sThumbLX, _state.Gamepad.sThumbLY); }
        }

        /// <summary>
        ///     Gets the RightThumbStick.
        /// </summary>
        public Vector2 RightThumbStick
        {
            get { return new Vector2(_state.Gamepad.sThumbRX, _state.Gamepad.sThumbRY); }
        }
    }
}