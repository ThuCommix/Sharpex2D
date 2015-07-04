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

namespace Sharpex2D.Framework.Input.Implementation.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Flags]
    internal enum ButtonFlags
    {
        /// <summary>
        /// D-Pad Up.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_UP = 0x0001,

        /// <summary>
        /// D-Pad Down.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_DOWN = 0x0002,

        /// <summary>
        /// D-Pad Left.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_LEFT = 0x0004,

        /// <summary>
        /// D-Pad Right.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_RIGHT = 0x0008,

        /// <summary>
        /// Start.
        /// </summary>
        XINPUT_GAMEPAD_START = 0x0010,

        /// <summary>
        /// Back.
        /// </summary>
        XINPUT_GAMEPAD_BACK = 0x0020,

        /// <summary>
        /// Left Thumb.
        /// </summary>
        XINPUT_GAMEPAD_LEFT_THUMB = 0x0040,

        /// <summary>
        /// Right Thumb.
        /// </summary>
        XINPUT_GAMEPAD_RIGHT_THUMB = 0x0080,

        /// <summary>
        /// Left Shoulder.
        /// </summary>
        XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100,

        /// <summary>
        /// Right Shoulder.
        /// </summary>
        XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200,

        /// <summary>
        /// Guide.
        /// </summary>
        XINPUT_GUIDE = 0x0400,

        /// <summary>
        /// A.
        /// </summary>
        XINPUT_GAMEPAD_A = 0x1000,

        /// <summary>
        /// B.
        /// </summary>
        XINPUT_GAMEPAD_B = 0x2000,

        /// <summary>
        /// X.
        /// </summary>
        XINPUT_GAMEPAD_X = 0x4000,

        /// <summary>
        /// Y.
        /// </summary>
        XINPUT_GAMEPAD_Y = 0x8000
    };
}