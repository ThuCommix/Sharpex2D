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
using System.Runtime.InteropServices;

namespace Sharpex2D.Input.Implementation.Touch
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class TouchInterops
    {
        internal const int WM_TOUCH = 0x0240;

        /// <summary>
        /// Registers the TouchWindow.
        /// </summary>
        /// <param name="hWnd">The Handle.</param>
        /// <param name="ulFlags">The Flags.</param>
        /// <returns>True on success.</returns>
        [DllImport("User32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterTouchWindow(IntPtr hWnd, UInt32 ulFlags);

        /// <summary>
        /// Gets the TouchInputInfo.
        /// </summary>
        /// <param name="hTouchInput">The TouchInput.</param>
        /// <param name="cInputs">The Number of structures.</param>
        /// <param name="pInputs">The TouchInput structure.</param>
        /// <param name="cbSize">The Size.</param>
        /// <returns>True on success.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [In, Out] TouchInput[] pInputs,
            int cbSize);

        /// <summary>
        /// Closes the TouchInputHandle.
        /// </summary>
        /// <param name="lParam">The Handle.</param>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseTouchInputHandle(IntPtr lParam);

        /// <summary>
        /// Closes the TouchInputHandle.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        [DllImport("User32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterTouchWindow(IntPtr handle);
    }
}