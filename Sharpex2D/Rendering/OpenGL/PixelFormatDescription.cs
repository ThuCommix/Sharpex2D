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

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Flags]
    internal enum PixelFormatDescription
    {
        /// <summary>
        /// DoubleBuffer.
        /// </summary>
        DoubleBuffer = 0x00000001,

        /// <summary>
        /// Stereo.
        /// </summary>
        Stereo = 0x00000002,

        /// <summary>
        /// Draw to window.
        /// </summary>
        DrawToWindow = 0x00000004,

        /// <summary>
        /// Draw to bitmap.
        /// </summary>
        DrawToBitmap = 0x00000008,

        /// <summary>
        /// Support GDI.
        /// </summary>
        SupportGDI = 0x00000010,

        /// <summary>
        /// Support OpenGL.
        /// </summary>
        SupportOpenGL = 0x00000020,

        /// <summary>
        /// Requests a generic format.
        /// </summary>
        GenericFormat = 0x00000040,

        /// <summary>
        /// Need palette.
        /// </summary>
        NeedPalette = 0x00000080,

        /// <summary>
        /// Need system palette.
        /// </summary>
        NeedSystemPalette = 0x00000100,

        /// <summary>
        /// Swap exchange.
        /// </summary>
        SwapExchange = 0x00000200,

        /// <summary>
        /// Swap copy.
        /// </summary>
        SwapCopy = 0x00000400,

        /// <summary>
        /// Swap layer buffers.
        /// </summary>
        SwapLayerBuffers = 0x00000800,

        /// <summary>
        /// Generic accelerated.
        /// </summary>
        GenericAccelerated = 0x00001000,

        /// <summary>
        /// Supports direct draw.
        /// </summary>
        SupportDirectDraw = 0x00002000
    }
}
