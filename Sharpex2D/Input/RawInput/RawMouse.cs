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

namespace Sharpex2D.Input.RawInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Explicit)]
    internal struct RawMouse
    {
        /// <summary>
        ///     The Flags.
        /// </summary>
        [FieldOffset(0)] public ushort usFlags;

        /// <summary>
        ///     The Buttons.
        /// </summary>
        [FieldOffset(4)] public uint ulButtons;

        /// <summary>
        ///     The ButtonFlags.
        /// </summary>
        [FieldOffset(4)] public ushort usButtonFlags;

        /// <summary>
        ///     The ButtonData.
        /// </summary>
        [FieldOffset(6)] public ushort usButtonData;

        /// <summary>
        ///     The RawButtons.
        /// </summary>
        [FieldOffset(8)] public uint ulRawButtons;

        /// <summary>
        ///     X Position.
        /// </summary>
        [FieldOffset(12)] public int lLastX;

        /// <summary>
        ///     Y Position.
        /// </summary>
        [FieldOffset(16)] public int lLastY;

        /// <summary>
        ///     The ExtraInformations.
        /// </summary>
        [FieldOffset(20)] public uint ulExtraInformation;
    }
}