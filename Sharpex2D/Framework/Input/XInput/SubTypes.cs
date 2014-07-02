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

namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Flags]
    public enum Subtypes
    {
        /// <summary>
        ///     Unknown.
        /// </summary>
        XINPUT_DEVSUBTYPE_UNKNOWN = 0x00,

        /// <summary>
        ///     Wheel.
        /// </summary>
        XINPUT_DEVSUBTYPE_WHEEL = 0x02,

        /// <summary>
        ///     Arcade stick.
        /// </summary>
        XINPUT_DEVSUBTYPE_ARCADE_STICK = 0x03,

        /// <summary>
        ///     Flight stick.
        /// </summary>
        XINPUT_DEVSUBTYPE_FLIGHT_STICK = 0x04,

        /// <summary>
        ///     Dance pad.
        /// </summary>
        XINPUT_DEVSUBTYPE_DANCE_PAD = 0x05,

        /// <summary>
        ///     Guitar.
        /// </summary>
        XINPUT_DEVSUBTYPE_GUITAR = 0x06,

        /// <summary>
        ///     Guitar alternate.
        /// </summary>
        XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE = 0x07,

        /// <summary>
        ///     Drum kit.
        /// </summary>
        XINPUT_DEVSUBTYPE_DRUM_KIT = 0x08,

        /// <summary>
        ///     Guitar bass.
        /// </summary>
        XINPUT_DEVSUBTYPE_GUITAR_BASS = 0x0B,

        /// <summary>
        ///     Arcade pad.
        /// </summary>
        XINPUT_DEVSUBTYPE_ARCADE_PAD = 0x13
    };
}