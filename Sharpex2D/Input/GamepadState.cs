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

using Sharpex2D.Math;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GamepadState
    {
        /// <summary>
        /// Initializes a new GamepadState class.
        /// </summary>
        /// <param name="dpadUp">The DPadUp State.</param>
        /// <param name="dpadDown">The DPadDown State.</param>
        /// <param name="dpadLeft">The DPadLeft State.</param>
        /// <param name="dpadRight">The DPadRight State.</param>
        /// <param name="aPressed">The A button State.</param>
        /// <param name="bPressed">The B button State.</param>
        /// <param name="yPressed">The Y button State.</param>
        /// <param name="xPressed">The X button State.</param>
        /// <param name="backPressed">The Back button State.</param>
        /// <param name="startPressed">The Start button State.</param>
        /// <param name="leftShoulder">The LeftShoulder Button.</param>
        /// <param name="rightShoulder">The RightShoulder Button.</param>
        /// <param name="leftStick">The LeftstickState.</param>
        /// <param name="rightStick">The RightstickState</param>
        /// <param name="leftTrigger">The LeftTrigger.</param>
        /// <param name="rightTrigger">The RightTrigger.</param>
        /// <param name="thumbLeft">The Left Thumbstick Position.</param>
        /// <param name="thumbRight">The Right Thumbstick Position.</param>
        public GamepadState(bool dpadUp, bool dpadDown, bool dpadLeft, bool dpadRight, bool aPressed, bool bPressed,
            bool yPressed, bool xPressed, bool backPressed, bool startPressed, bool leftShoulder, bool rightShoulder,
            bool leftStick, bool rightStick, int leftTrigger, int rightTrigger, Vector2 thumbLeft, Vector2 thumbRight)
        {
            IsDPadUpPressed = dpadUp;
            IsDPadDownPressed = dpadDown;
            IsDPadLeftPressed = dpadLeft;
            IsDPadRightPressed = dpadRight;
            IsAPressed = aPressed;
            IsBPressed = bPressed;
            IsYPressed = yPressed;
            IsXPressed = xPressed;
            IsBackPressed = backPressed;
            IsStartPressed = startPressed;
            IsLeftShoulderPressed = leftShoulder;
            IsRightShoulderPressed = rightShoulder;
            IsLeftStickPressed = leftStick;
            IsRightStickPressed = rightStick;
            LeftTrigger = leftTrigger;
            RightTrigger = rightTrigger;
            LeftThumbStick = new Vector2(thumbLeft.X, thumbLeft.Y);
            RightThumbStick = new Vector2(thumbRight.X, thumbRight.Y);
        }

        /// <summary>
        /// A value indicating whether the D-Pad up is pressed.
        /// </summary>
        public bool IsDPadUpPressed { get; private set; }

        /// <summary>
        /// A value indicating whether the D-Pad down is pressed.
        /// </summary>
        public bool IsDPadDownPressed { get; private set; }

        /// <summary>
        /// A value indicating whether the D-Pad left is pressed.
        /// </summary>
        public bool IsDPadLeftPressed { get; private set; }

        /// <summary>
        /// A value indicating whether the D-Pad right is pressed.
        /// </summary>
        public bool IsDPadRightPressed { get; private set; }

        /// <summary>
        /// A value indicating whether A is pressed.
        /// </summary>
        public bool IsAPressed { get; private set; }

        /// <summary>
        /// A value indicating whether B is pressed.
        /// </summary>
        public bool IsBPressed { get; private set; }

        /// <summary>
        /// A value indicating whether X is pressed.
        /// </summary>
        public bool IsXPressed { get; private set; }

        /// <summary>
        /// A value indicating whether Y is pressed.
        /// </summary>
        public bool IsYPressed { get; private set; }

        /// <summary>
        /// A value indicating whether back is pressed.
        /// </summary>
        public bool IsBackPressed { get; private set; }

        /// <summary>
        /// A value indicating whether start is pressed.
        /// </summary>
        public bool IsStartPressed { get; private set; }

        /// <summary>
        /// A value indicating whether left-shoulder is pressed.
        /// </summary>
        public bool IsLeftShoulderPressed { get; private set; }

        /// <summary>
        /// A value indicating whether right-shoulder is pressed.
        /// </summary>
        public bool IsRightShoulderPressed { get; private set; }

        /// <summary>
        /// A value indicating whether left-stick is pressed.
        /// </summary>
        public bool IsLeftStickPressed { get; private set; }

        /// <summary>
        /// A value indicating whether right-stick is pressed.
        /// </summary>
        public bool IsRightStickPressed { get; private set; }

        /// <summary>
        /// A value indicating whether left-trigger is pressed.
        /// </summary>
        public int LeftTrigger { get; private set; }

        /// <summary>
        /// A value indicating whether right-trigger is pressed.
        /// </summary>
        public int RightTrigger { get; private set; }

        /// <summary>
        /// Gets the LeftThumbStick.
        /// </summary>
        public Vector2 LeftThumbStick { get; private set; }

        /// <summary>
        /// Gets the RightThumbStick.
        /// </summary>
        public Vector2 RightThumbStick { get; private set; }
    }
}