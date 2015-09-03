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

namespace Sharpex2D.Framework.Input
{
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
            _states = new Dictionary<GamepadButtons, bool>
            {
                {GamepadButtons.Up, dpadUp},
                {GamepadButtons.Down, dpadDown},
                {GamepadButtons.Left, dpadLeft},
                {GamepadButtons.Right, dpadRight},
                {GamepadButtons.A, aPressed},
                {GamepadButtons.B, bPressed},
                {GamepadButtons.Y, yPressed},
                {GamepadButtons.X, xPressed},
                {GamepadButtons.Back, backPressed},
                {GamepadButtons.Start, startPressed},
                {GamepadButtons.LeftShoulder, leftShoulder},
                {GamepadButtons.RightShoulder, rightShoulder},
                {GamepadButtons.LeftStick, leftStick},
                {GamepadButtons.RightStick, rightStick},
                {GamepadButtons.Guide, guide}
            };

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
        /// Applies the dead zones.
        /// </summary>
        /// <param name="leftThumbStick">The LeftThumbStickDeadZone.</param>
        /// <param name="rightThumbStick">The RightThumbStickDeadZone.</param>
        /// <param name="trigger">The TriggerDeadZone.</param>
        internal void ApplyDeadZones(float leftThumbStick, float rightThumbStick, float trigger)
        {
            if (trigger > 0)
            {
                if (LeftTrigger < trigger)
                {
                    LeftTrigger = 0;
                }

                if (RightTrigger < trigger)
                {
                    RightTrigger = 0;
                }
            }

            if (leftThumbStick > 0)
            {
                if (LeftThumbStick.X > 0 && LeftThumbStick.X < leftThumbStick)
                {
                    LeftThumbStick = new Vector2(0, LeftThumbStick.Y);
                }

                if (LeftThumbStick.Y > 0 && LeftThumbStick.Y < leftThumbStick)
                {
                    LeftThumbStick = new Vector2(LeftThumbStick.X, 0);
                }

                if (LeftThumbStick.X < 0 && LeftThumbStick.X > -leftThumbStick)
                {
                    LeftThumbStick = new Vector2(0, LeftThumbStick.Y);
                }

                if (LeftThumbStick.Y < 0 && LeftThumbStick.Y > -leftThumbStick)
                {
                    LeftThumbStick = new Vector2(LeftThumbStick.X, 0);
                }
            }

            if (rightThumbStick > 0)
            {
                if (RightThumbStick.X > 0 && RightThumbStick.X < rightThumbStick)
                {
                    RightThumbStick = new Vector2(0, RightThumbStick.Y);
                }

                if (RightThumbStick.Y > 0 && RightThumbStick.Y < rightThumbStick)
                {
                    RightThumbStick = new Vector2(RightThumbStick.X, 0);
                }

                if (RightThumbStick.X < 0 && RightThumbStick.X > -rightThumbStick)
                {
                    RightThumbStick = new Vector2(0, RightThumbStick.Y);
                }

                if (RightThumbStick.Y < 0 && RightThumbStick.Y > -rightThumbStick)
                {
                    RightThumbStick = new Vector2(RightThumbStick.X, 0);
                }
            }
        }

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
