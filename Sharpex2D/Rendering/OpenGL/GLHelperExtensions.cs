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

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal static class GLHelperExtensions
    {
        /// <summary>
        /// Converts the blend to constant
        /// </summary>
        /// <param name="blend">The blend</param>
        /// <returns>Returns the opengl constant</returns>
        public static uint ToConstant(this Blend blend)
        {
            switch (blend)
            {
                case Blend.One:
                    return GLInterops.GLOne;
                case Blend.Zero:
                    return GLInterops.GLZero;
                case Blend.SourceColor:
                    return GLInterops.GLSrcColor;
                case Blend.InverseSourceColor:
                    return GLInterops.GLOneMinusSrcColor;
                case Blend.SourceAlpha:
                    return GLInterops.GLSrcAlpha;
                case Blend.InverseSourceAlpha:
                    return GLInterops.GLOneMinusSrcAlpha;
                case Blend.DestinationColor:
                    return GLInterops.GLDstColor;
                case Blend.InverseDestinationColor:
                    return GLInterops.GLOneMinusDstColor;
                case Blend.DestinationAlpha:
                    return GLInterops.GLDstAlpha;
                case Blend.InverseDestinationAlpha:
                    return GLInterops.GLOneMinusDstAlpha;
                case Blend.SourceAlphaSaturation:
                    return GLInterops.GLSrcAlphaSaturate;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blend), blend, null);
            }
        }

        /// <summary>
        /// Converts the blend to constant
        /// </summary>
        /// <param name="function">The blend function</param>
        /// <returns>Returns the opengl constant</returns>
        public static uint ToConstant(this BlendFunction function)
        {
            switch (function)
            {
                case BlendFunction.Add:
                    return GLInterops.GLFuncAdd;
                case BlendFunction.Subtract:
                    return GLInterops.GLFuncSubtract;
                case BlendFunction.ReverseSubtract:
                    return GLInterops.GLFuncReverseSubtract;
                case BlendFunction.Min:
                    return GLInterops.GLMin;
                case BlendFunction.Max:
                    return GLInterops.GLMax;
                default:
                    throw new ArgumentOutOfRangeException(nameof(function), function, null);
            }
        }
    }
}
