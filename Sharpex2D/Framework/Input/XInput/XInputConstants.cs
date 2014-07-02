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

namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class XInputConstants
    {
        /// <summary>
        ///     Devtype Gamepad.
        /// </summary>
        public const int XINPUT_DEVTYPE_GAMEPAD = 0x01;

        /// <summary>
        ///     Subtype gamepad.
        /// </summary>
        public const int XINPUT_DEVSUBTYPE_GAMEPAD = 0x01;

        /// <summary>
        ///     Threshold Left thumb deadzone.
        /// </summary>
        public const int XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849;

        /// <summary>
        ///     Threshold Right thumb deadzone.
        /// </summary>
        public const int XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;

        /// <summary>
        ///     Threshold trigger deadzone.
        /// </summary>
        public const int XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30;

        /// <summary>
        ///     Flag Gamepad.
        /// </summary>
        public const int XINPUT_FLAG_GAMEPAD = 0x00000001;
    }
}