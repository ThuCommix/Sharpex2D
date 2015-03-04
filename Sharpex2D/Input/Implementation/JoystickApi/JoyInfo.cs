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

namespace Sharpex2D.Input.Implementation.JoystickApi
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    [StructLayout(LayoutKind.Sequential)]
    internal struct JoyInfo
    {
        /// <summary>
        /// The X position.
        /// </summary>
        public uint wXpos;

        /// <summary>
        /// The Y position.
        /// </summary>
        public uint wYpos;

        /// <summary>
        /// The Z position.
        /// </summary>
        public uint wZpos;

        /// <summary>
        /// The ButtonStates.
        /// </summary>
        public uint wButtons;
    }
}