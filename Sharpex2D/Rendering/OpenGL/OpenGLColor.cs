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

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal struct OpenGLColor
    {
        /// <summary>
        /// Initializes a new OpenGLColor struct.
        /// </summary>
        /// <param name="r">The R value.</param>
        /// <param name="g">The G value.</param>
        /// <param name="b">The B value.</param>
        public OpenGLColor(float r, float g, float b) : this(1, r, g, b)
        {
        }

        /// <summary>
        /// Initializes a new OpenGLColor struct.
        /// </summary>
        /// <param name="a">The A value.</param>
        /// <param name="r">The R value.</param>
        /// <param name="g">The G value.</param>
        /// <param name="b">The B value.</param>
        public OpenGLColor(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Gets the A value.
        /// </summary>
        public float A { get; }

        /// <summary>
        /// Gets the R value.
        /// </summary>
        public float R { get; }

        /// <summary>
        /// Gets the G value.
        /// </summary>
        public float G { get; }

        /// <summary>
        /// Gets the B value.
        /// </summary>
        public float B { get; }

        /// <summary>
        /// Converts the OpenGLColor to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("(R: {0} G: {1} B: {2} A: {3})", R, G, B, A);
        }
    }
}
