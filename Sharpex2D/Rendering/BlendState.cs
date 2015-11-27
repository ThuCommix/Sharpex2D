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

namespace Sharpex2D.Framework.Rendering
{
    public class BlendState
    {
        /// <summary>
        /// Gets the source blend
        /// </summary>
        public Blend SourceBlend { private set; get; }

        /// <summary>
        /// Gets the destination blend
        /// </summary>
        public Blend DestinationBlend { private set; get; }

        /// <summary>
        /// Gets the blend function
        /// </summary>
        public BlendFunction BlendFunction { private set; get; }

        /// <summary>
        /// Adding the destination data to the source data without using alpha
        /// </summary>
        public static readonly BlendState Additive = new BlendState(Blend.SourceAlpha, Blend.One, BlendFunction.Add);

        /// <summary>
        /// Blending the source and destination data using alpha
        /// </summary>
        public static readonly BlendState AlphaBlend = new BlendState(Blend.One, Blend.InverseSourceAlpha, BlendFunction.Add);

        /// <summary>
        /// Blending source and destination data by using alpha while assuming the color data contains no alpha information
        /// </summary>
        public static readonly BlendState NonPremultiplied = new BlendState(Blend.SourceAlpha, Blend.InverseSourceAlpha, BlendFunction.Add);

        /// <summary>
        /// Overwriting the source with the destination data
        /// </summary>
        public static readonly BlendState Opaque = new BlendState(Blend.One, Blend.Zero, BlendFunction.Add);

        /// <summary>
        /// Initializes a new BlendState class
        /// </summary>
        /// <param name="srcBlend">The source blend</param>
        /// <param name="destBlend">The destination blend</param>
        /// <param name="function">The blend function</param>
        public BlendState(Blend srcBlend, Blend destBlend, BlendFunction function)
        {
            SourceBlend = srcBlend;
            DestinationBlend = destBlend;
            BlendFunction = function;
        }
    }
}
