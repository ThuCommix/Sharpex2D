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

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    internal static class DirectXHelperExtensions
    {
        /// <summary>
        /// Converts the blend to SharpDX.Direct3D9.Blend
        /// </summary>
        /// <param name="blend">The blend</param>
        /// <returns>Returns the SharpDX.Direct3D9.Blend</returns>
        public static SharpDX.Direct3D9.Blend ToDirectXBlend(this Blend blend)
        {
            switch (blend)
            {
                case Blend.One:
                    return SharpDX.Direct3D9.Blend.One;
                case Blend.Zero:
                    return SharpDX.Direct3D9.Blend.Zero;
                case Blend.SourceColor:
                    return SharpDX.Direct3D9.Blend.SourceColor;
                case Blend.InverseSourceColor:
                    return SharpDX.Direct3D9.Blend.InverseSourceColor;
                case Blend.SourceAlpha:
                    return SharpDX.Direct3D9.Blend.SourceAlpha;
                case Blend.InverseSourceAlpha:
                    return SharpDX.Direct3D9.Blend.InverseSourceAlpha;
                case Blend.DestinationColor:
                    return SharpDX.Direct3D9.Blend.DestinationColor;
                case Blend.InverseDestinationColor:
                    return SharpDX.Direct3D9.Blend.InverseDestinationColor;
                case Blend.DestinationAlpha:
                    return SharpDX.Direct3D9.Blend.DestinationAlpha;
                case Blend.InverseDestinationAlpha:
                    return SharpDX.Direct3D9.Blend.InverseDestinationAlpha;
                case Blend.SourceAlphaSaturation:
                    return SharpDX.Direct3D9.Blend.SourceAlphaSaturated;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blend), blend, null);
            }
        }

        /// <summary>
        /// Converts the blend function into SharpDX.Direct3D9.BlendOperation
        /// </summary>
        /// <param name="function">The blend function</param>
        /// <returns>Returns the SharpDX.Direct3D9.BlendOperation</returns>
        public static SharpDX.Direct3D9.BlendOperation ToDirectXBlendFunction(this BlendFunction function)
        {
            switch (function)
            {
                case BlendFunction.Add:
                    return SharpDX.Direct3D9.BlendOperation.Add;
                case BlendFunction.Subtract:
                    return SharpDX.Direct3D9.BlendOperation.Subtract;
                case BlendFunction.ReverseSubtract:
                    return SharpDX.Direct3D9.BlendOperation.ReverseSubtract;
                case BlendFunction.Min:
                    return SharpDX.Direct3D9.BlendOperation.Minimum;
                case BlendFunction.Max:
                    return SharpDX.Direct3D9.BlendOperation.Maximum;
                default:
                    throw new ArgumentOutOfRangeException(nameof(function), function, null);
            }
        }
    }
}
