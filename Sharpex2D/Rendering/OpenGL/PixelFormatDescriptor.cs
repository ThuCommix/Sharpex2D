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
using System.Runtime.InteropServices;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Sequential)]
    internal class PixelFormatDescriptor
    {
        /// <summary>
        /// Specifies the size of this data structure. This value should be set to <c>sizeof(PIXELFORMATDESCRIPTOR)</c>.
        /// </summary>
        public Int16 Size;

        /// <summary>
        /// Specifies the version of this data structure. This value should be set to 1.
        /// </summary>
        public Int16 Version;

        /// <summary>
        /// A set of bit flags that specify properties of the pixel buffer. The properties are generally not mutually exclusive;
        /// you can set any combination of bit flags, with the exceptions noted.
        /// </summary>
        public PixelFormatDescription FormatDescription;

        /// <summary>
        /// Specifies the type of pixel data. The following types are defined.
        /// </summary>
        public int PixelType;

        /// <summary>
        /// Specifies the number of color bitplanes in each color buffer. For RGBA pixel types, it is the size
        /// of the color buffer, excluding the alpha bitplanes. For color-index pixels, it is the size of the
        /// color-index buffer.
        /// </summary>
        public Byte ColorBits;

        /// <summary>
        /// Specifies the number of red bitplanes in each RGBA color buffer.
        /// </summary>
        public Byte RedBits;

        /// <summary>
        /// Specifies the shift count for red bitplanes in each RGBA color buffer.
        /// </summary>
        public Byte RedShift;

        /// <summary>
        /// Specifies the number of green bitplanes in each RGBA color buffer.
        /// </summary>
        public Byte GreenBits;

        /// <summary>
        /// Specifies the shift count for green bitplanes in each RGBA color buffer.
        /// </summary>
        public Byte GreenShift;

        /// <summary>
        /// Specifies the number of blue bitplanes in each RGBA color buffer.
        /// </summary>
        public Byte BlueBits;

        /// <summary>
        /// Specifies the shift count for blue bitplanes in each RGBA color buffer.
        /// </summary>
        public Byte BlueShift;

        /// <summary>
        /// Specifies the number of alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
        /// </summary>
        public Byte AlphaBits;

        /// <summary>
        /// Specifies the shift count for alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
        /// </summary>
        public Byte AlphaShift;

        /// <summary>
        /// Specifies the total number of bitplanes in the accumulation buffer.
        /// </summary>
        public Byte AccumBits;

        /// <summary>
        /// Specifies the number of red bitplanes in the accumulation buffer.
        /// </summary>
        public Byte AccumRedBits;

        /// <summary>
        /// Specifies the number of green bitplanes in the accumulation buffer.
        /// </summary>
        public Byte AccumGreenBits;

        /// <summary>
        /// Specifies the number of blue bitplanes in the accumulation buffer.
        /// </summary>
        public Byte AccumBlueBits;

        /// <summary>
        /// Specifies the number of alpha bitplanes in the accumulation buffer.
        /// </summary>
        public Byte AccumAlphaBits;

        /// <summary>
        /// Specifies the depth of the depth (z-axis) buffer.
        /// </summary>
        public Byte DepthBits;

        /// <summary>
        /// Specifies the depth of the stencil buffer.
        /// </summary>
        public Byte StencilBits;

        /// <summary>
        /// Specifies the number of auxiliary buffers. Auxiliary buffers are not supported.
        /// </summary>
        public Byte AuxBuffers;

        /// <summary>
        /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
        /// </summary>
        /// <remarks>Specifies the type of layer.</remarks>
        public int LayerType;

        /// <summary>
        /// Specifies the number of overlay and underlay planes. Bits 0 through 3 specify up to 15 overlay planes and
        /// bits 4 through 7 specify up to 15 underlay planes.
        /// </summary>
        public Byte Reserved;

        /// <summary>
        /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
        /// </summary>
        /// <remarks>
        ///		Specifies the layer mask. The layer mask is used in conjunction with the visible mask to determine
        ///		if one layer overlays another.
        /// </remarks>
        public Int32 LayerMask;

        /// <summary>
        /// Specifies the transparent color or index of an underlay plane. When the pixel type is RGBA, <b>dwVisibleMask</b>
        /// is a transparent RGB color value. When the pixel type is color index, it is a transparent index value.
        /// </summary>
        public Int32 VisibleMask;

        /// <summary>
        /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
        /// </summary>
        /// <remarks>
        ///		Specifies whether more than one pixel format shares the same frame buffer. If the result of the bitwise
        ///		AND of the damage masks between two pixel formats is nonzero, then they share the same buffers.
        /// </remarks>
        public Int32 DamageMask;
    }
}