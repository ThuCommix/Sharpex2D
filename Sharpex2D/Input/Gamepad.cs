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

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class Gamepad
    {
        /// <summary>
        /// Gets the GamepadState.
        /// </summary>
        /// <param name="playerIndex">The PlayerIndex.</param>
        /// <returns>GamepadState.</returns>
        public static GamepadState GetState(PlayerIndex playerIndex)
        {
            return SGL.InputManager.GetInputs<NativeInput<GamepadState>>()[(int) playerIndex].GetState();
        }

        /// <summary>
        /// Vibrates the Gamepad.
        /// </summary>
        /// <param name="playerIndex">The PlayerIndex.</param>
        /// <param name="left">The Left.</param>
        /// <param name="right">The Right.</param>
        /// <param name="length">The Length.</param>
        public static void Vibrate(PlayerIndex playerIndex, float left, float right, float length)
        {
            var gamepad = SGL.InputManager.GetInputs<NativeInput<GamepadState>>()[(int) playerIndex] as IGamepad;
            if (gamepad != null)
            {
                gamepad.Vibrate(left, right, length);
            }
            else
            {
                throw new NotSupportedException("The gamepad does not support vibration.");
            }
        }

        /// <summary>
        /// Gets the BatteryLevel.
        /// </summary>
        /// <param name="playerIndex">The PlayerIndex.</param>
        /// <returns>BatteryPercentage.</returns>
        public static BatteryLevel BatteryLevel(PlayerIndex playerIndex)
        {
            var gamepad = SGL.InputManager.GetInputs<NativeInput<GamepadState>>()[(int) playerIndex] as IGamepad;
            if (gamepad != null)
            {
                return gamepad.BatteryLevel;
            }

            throw new NotSupportedException("The gamepad does not support battery levels.");
        }
    }
}