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

using SharpDX;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class DirectXHelper
    {
        /// <summary>
        /// Converts the Sharpex2D.Rendering.Color class into a SharpDX.ColorBGRA class.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>ColorBGRA.</returns>
        public static ColorBGRA ConvertColor(Color color)
        {
            return new ColorBGRA(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Converts the Sharpex2D.Math.Rectangle class into a SharpDX.Rectangle class.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Rectangle.</returns>
        public static SharpDX.Rectangle ConvertRectangle(Rectangle rectangle)
        {
            return new SharpDX.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                (int) rectangle.Height);
        }

        /// <summary>
        /// Converts the Sharpex2D.Math.Vector2 class into a SharpDX.Vector3 class.
        /// </summary>
        /// <param name="vector2">The Vector2.</param>
        /// <returns>Vector3.</returns>
        public static Vector3 ConvertVector2(Vector2 vector2)
        {
            return new Vector3(vector2.X, vector2.Y, 0);
        }
    }
}