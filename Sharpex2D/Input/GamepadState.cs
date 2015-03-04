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

using System.Collections.Generic;
using Sharpex2D.Math;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GamepadState
    {
        private readonly Dictionary<GamepadButtons, bool> _states;

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
        /// <param name="guide">The Guide State.</param>
        /// <param name="leftTrigger">The LeftTrigger.</param>
        /// <param name="rightTrigger">The RightTrigger.</param>
        /// <param name="thumbLeft">The Left Thumbstick Position.</param>
        /// <param name="thumbRight">The Right Thumbstick Position.</param>
        internal GamepadState(bool dpadUp, bool dpadDown, bool dpadLeft, bool dpadRight, bool aPressed, bool bPressed,
            bool yPressed, bool xPressed, bool backPressed, bool startPressed, bool leftShoulder, bool rightShoulder,
            bool leftStick, bool rightStick, bool guide, float leftTrigger, float rightTrigger, Vector2 thumbLeft,
            Vector2 thumbRight)
        {
            _states = new Dictionary<GamepadButtons, bool>();
            _states.Add(GamepadButtons.Up, dpadUp);
            _states.Add(GamepadButtons.Down, dpadDown);
            _states.Add(GamepadButtons.Left, dpadLeft);
            _states.Add(GamepadButtons.Right, dpadRight);
            _states.Add(GamepadButtons.A, aPressed);
            _states.Add(GamepadButtons.B, bPressed);
            _states.Add(GamepadButtons.Y, yPressed);
            _states.Add(GamepadButtons.X, xPressed);
            _states.Add(GamepadButtons.Back, backPressed);
            _states.Add(GamepadButtons.Start, startPressed);
            _states.Add(GamepadButtons.LeftShoulder, leftShoulder);
            _states.Add(GamepadButtons.RightShoulder, rightShoulder);
            _states.Add(GamepadButtons.LeftStick, leftStick);
            _states.Add(GamepadButtons.RightStick, rightStick);
            _states.Add(GamepadButtons.Guide, guide);

            LeftTrigger = leftTrigger;
            RightTrigger = rightTrigger;
            LeftThumbStick = new Vector2(thumbLeft.X, thumbLeft.Y);
            RightThumbStick = new Vector2(thumbRight.X, thumbRight.Y);
        }

        /// <summary>
        /// A value indicating whether left-trigger is pressed.
        /// </summary>
        public float LeftTrigger { get; private set; }

        /// <summary>
        /// A value indicating whether right-trigger is pressed.
        /// </summary>
        public float RightTrigger { get; private set; }

        /// <summary>
        /// Gets the LeftThumbStick.
        /// </summary>
        public Vector2 LeftThumbStick { get; private set; }

        /// <summary>
        /// Gets the RightThumbStick.
        /// </summary>
        public Vector2 RightThumbStick { get; private set; }

        /// <summary>
        /// A value indicating whether the specified button is pressed.
        /// </summary>
        /// <param name="button">The GamepadButton.</param>
        /// <returns>True if currently pressed.</returns>
        public bool IsPressed(GamepadButtons button)
        {
            return _states[button];
        }
    }
}