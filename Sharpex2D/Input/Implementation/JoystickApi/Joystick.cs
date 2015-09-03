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

namespace Sharpex2D.Framework.Input.Implementation.JoystickApi
{
    internal class Joystick : IJoystick
    {
        /// <summary>
        /// A value indicating whether a Joystick is available.
        /// </summary>
        public bool IsAvailable => JoystickInterops.joyGetNumDevs() > 0;

        /// <summary>
        /// Gets the State.
        /// </summary>
        /// <returns>JoystickState.</returns>
        public JoystickState GetState()
        {
            if (JoystickInterops.joyGetNumDevs() > 0)
            {
                var jie = new JoyInfoEx {dwFlags = (int) JoyFlags.JOY_RETURNALL};
                jie.dwSize = (uint) Marshal.SizeOf(jie);

                Try(JoystickInterops.joyGetPosEx(0, ref jie));

                var buttonStates = new Dictionary<int, bool>();

                for (int i = 1; i <= 32; i++)
                {
                    int button = 2 ^ (i - 1);
                    buttonStates.Add(i, ((jie.dwButtons & button) != 0));
                }

                return new JoystickState(jie.dwXpos, jie.dwYpos, jie.dwZpos, jie.dwRpos, jie.dwUpos, jie.dwVpos,
                    new PointOfView(jie.dwPOV/100d), buttonStates);
            }

            throw new InvalidOperationException("Joystick not connected.");
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
        /// Trys an MMResult.
        /// </summary>
        /// <param name="result">The Result.</param>
        internal static void Try(uint result)
        {
            switch (result)
            {
                case 2:
                    throw new InvalidOperationException("An Invalid JoyStickId was passed to the driver.");
                case 6:
                    throw new InvalidOperationException("The Joystick driver is not present.");
                case 11:
                    throw new InvalidOperationException("An Invalidparam was passed to the driver.");
                case 165:
                    throw new InvalidOperationException("An Invalid joystick param was passed to the driver.");
                case 167:
                    throw new InvalidOperationException("The Joystick is unplugged.");
            }
        }
    }
}
