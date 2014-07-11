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

namespace Sharpex2D.Input.RawInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal enum HidUsage : ushort
    {
        /// <summary>Unknown usage.</summary>
        Undefined = 0x00,

        /// <summary>Pointer</summary>
        Pointer = 0x01,

        /// <summary>Mouse</summary>
        Mouse = 0x02,

        /// <summary>Joystick</summary>
        Joystick = 0x04,

        /// <summary>Game Pad</summary>
        Gamepad = 0x05,

        /// <summary>Keyboard</summary>
        Keyboard = 0x06,

        /// <summary>Keypad</summary>
        Keypad = 0x07,

        /// <summary>Muilt-axis Controller</summary>
        SystemControl = 0x80,

        /// <summary>Tablet PC controls</summary>
        Tablet = 0x80,

        /// <summary>Consumer</summary>
        Consumer = 0x0C,
    }
}