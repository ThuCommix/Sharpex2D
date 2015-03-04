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

using System.Runtime.InteropServices;
using System.Security;

namespace Sharpex2D.Input.Implementation.JoystickApi
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class JoystickInterops
    {
        /// <summary>
        /// Gets the number of connected devices.
        /// </summary>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        internal static extern uint joyGetNumDevs();

        /// <summary>
        /// Gets the state of the Joystick.
        /// </summary>
        /// <param name="uJoyId">The Joystick Id.</param>
        /// <param name="pji">The JoyInfo.</param>
        /// <returns>UInt32.</returns>
        [DllImport("winmm.dll")]
        internal static extern uint joyGetPos(uint uJoyId, ref JoyInfo pji);

        /// <summary>
        /// Gets the state of the extended Joystick.
        /// </summary>
        /// <param name="uJoyId">The Joystick Id.</param>
        /// <param name="pjiex">The extended JoyInfo.</param>
        /// <returns>UInt32.</returns>
        [DllImport("winmm.dll"), SuppressUnmanagedCodeSecurity]
        internal static extern uint joyGetPosEx(uint uJoyId, ref JoyInfoEx pjiex);
    }
}