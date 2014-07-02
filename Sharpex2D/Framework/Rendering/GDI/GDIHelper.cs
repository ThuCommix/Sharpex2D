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

using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class GDIHelper
    {
        /// <summary>
        /// Converts the Color.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>GDI Color.</returns>
        public static System.Drawing.Color ConvertColor(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts the Point.
        /// </summary>
        /// <param name="vector">The Vector2.</param>
        /// <returns>Point.</returns>
        public static System.Drawing.Point ConvertPoint(Vector2 vector)
        {
            return new System.Drawing.Point((int) vector.X, (int) vector.Y);
        }

        /// <summary>
        /// Converts the PointF.
        /// </summary>
        /// <param name="vector">The Vector2.</param>
        /// <returns>PointF.</returns>
        public static System.Drawing.PointF ConvertPointF(Vector2 vector)
        {
            return new System.Drawing.PointF(vector.X, vector.Y);
        }

        /// <summary>
        /// Converts the Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Rectangle.</returns>
        public static System.Drawing.Rectangle ConvertRectangle(Rectangle rectangle)
        {
            return new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                (int) rectangle.Height);
        }

        /// <summary>
        /// Converts the RectangleF.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>RectangleF.</returns>
        public static System.Drawing.RectangleF ConvertRectangleF(Rectangle rectangle)
        {
            return new System.Drawing.RectangleF(rectangle.X, rectangle.Y, rectangle.Width,
                rectangle.Height);
        }
    }
}
