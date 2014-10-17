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

namespace Sharpex2D.Input.Windows.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class XInputAPI
    {
#if Windows

        /// <summary>
        /// Gets the XInput state.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="pState">The InputState.</param>
        /// <returns></returns>
        internal static int XInputGetState(int dwUserIndex, ref XInputState pState)
        {
            return NativeMethods.XInputGetState(dwUserIndex, ref pState);
        }

        /// <summary>
        /// Sets the Input state.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="pVibration">The Vibration.</param>
        /// <returns></returns>
        internal static int XInputSetState(int dwUserIndex, ref XInputVibration pVibration)
        {
            return NativeMethods.XInputSetState(dwUserIndex, ref pVibration);
        }

        /// <summary>
        /// Gets the Capabilities.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="dwFlags">The dwFlags.</param>
        /// <param name="pCapabilities">The Capabilities.</param>
        /// <returns></returns>
        internal static int XInputGetCapabilities(int dwUserIndex, int dwFlags, ref XInputCapabilities pCapabilities)
        {
            return NativeMethods.XInputGetCapabilities(dwUserIndex, dwFlags, ref pCapabilities);
        }

        /// <summary>
        /// Gets the Battery information.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="devType">The DevType.</param>
        /// <param name="pBatteryInformation">The BatteryInformation.</param>
        /// <returns></returns>
        internal static int XInputGetBatteryInformation(int dwUserIndex, byte devType,
            ref XInputBatteryInformation pBatteryInformation)
        {
            return NativeMethods.XInputGetBatteryInformation(dwUserIndex, devType, ref pBatteryInformation);
        }

#endif
    }
}