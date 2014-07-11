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

using System.Runtime.InteropServices;

namespace Sharpex2D.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputGamepad
    {
        /// <summary>
        ///     The Buttons.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(0)] public short wButtons;

        /// <summary>
        ///     The back left trigger.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(2)] public byte bLeftTrigger;

        /// <summary>
        ///     The back right trigger.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(3)] public byte bRightTrigger;

        /// <summary>
        ///     The thumb left X.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(4)] public short sThumbLX;

        /// <summary>
        ///     The thumb left Y.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(6)] public short sThumbLY;

        /// <summary>
        ///     The thumb right X.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(8)] public short sThumbRX;

        /// <summary>
        ///     The thumb right Y.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(10)] public short sThumbRY;

        /// <summary>
        ///     A value indicating whether the button was pressed.
        /// </summary>
        /// <param name="buttonFlags">The ButtonFlags.</param>
        /// <returns>True if the button was pressed.</returns>
        public bool IsButtonPressed(int buttonFlags)
        {
            return (wButtons & buttonFlags) == buttonFlags;
        }

        /// <summary>
        ///     A value indicating whether the button is accessable on the gamepad.
        /// </summary>
        /// <param name="buttonFlags">The ButtonFlags.</param>
        /// <returns>True if accessable.</returns>
        public bool IsButtonPresent(int buttonFlags)
        {
            return (wButtons & buttonFlags) == buttonFlags;
        }

        /// <summary>
        ///     Copies the source.
        /// </summary>
        /// <param name="source">The Source.</param>
        public void Copy(XInputGamepad source)
        {
            sThumbLX = source.sThumbLX;
            sThumbLY = source.sThumbLY;
            sThumbRX = source.sThumbRX;
            sThumbRY = source.sThumbRY;
            bLeftTrigger = source.bLeftTrigger;
            bRightTrigger = source.bRightTrigger;
            wButtons = source.wButtons;
        }
    }
}