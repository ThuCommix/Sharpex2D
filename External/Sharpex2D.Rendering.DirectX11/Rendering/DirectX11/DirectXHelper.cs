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

using SharpDX;
using SharpDX.Direct2D1;
using Rectangle = Sharpex2D.Math.Rectangle;

namespace Sharpex2D.Rendering.DirectX11
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class DirectXHelper
    {
        /// <summary>
        ///     Sets or gets the RenderTarget.
        /// </summary>
        public static RenderTarget RenderTarget { set; get; }

        /// <summary>
        ///     Sets or gets the D2DFactory.
        /// </summary>
        public static Factory D2DFactory { set; get; }

        /// <summary>
        ///     Sets or gets the DirectWriteFactory.
        /// </summary>
        public static SharpDX.DirectWrite.Factory DirectWriteFactory { set; get; }

        /// <summary>
        ///     Converts a Rectangle into DxRectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>DxRectangle.</returns>
        public static RectangleF ConvertRectangle(Rectangle rectangle)
        {
            return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        ///     Converts a Color into DxColor.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>DxColor.</returns>
        public static SharpDX.Color ConvertColor(Color color)
        {
            return new SharpDX.Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        ///     Converts a Color into a SolidColorBrush.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>SolidColorBrush</returns>
        public static SolidColorBrush ConvertSolidColorBrush(Color color)
        {
            return new SolidColorBrush(RenderTarget, ConvertColor(color));
        }

        /// <summary>
        ///     Converts a Vector into DxVector.
        /// </summary>
        /// <param name="vector">The Vector.</param>
        /// <returns>DxVector</returns>
        public static Vector2 ConvertVector(Math.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        ///     Converts a Rectangle into a Ellipse.
        /// </summary>
        /// <param name="ellipse">The Ellipse.</param>
        /// <returns>Ellipse</returns>
        public static Ellipse ConvertEllipse(Math.Ellipse ellipse)
        {
            var ellipseDx = new Ellipse
            {
                Point = ConvertVector(ellipse.Position),
                RadiusX = ellipse.RadiusX,
                RadiusY = ellipse.RadiusY
            };
            return ellipseDx;
        }
    }
}