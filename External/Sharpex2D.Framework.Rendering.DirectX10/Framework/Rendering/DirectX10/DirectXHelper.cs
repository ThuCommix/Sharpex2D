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

using System.Drawing;
using SlimDX;
using SlimDX.Direct2D;
using Brush = SlimDX.Direct2D.Brush;
using Vector2 = Sharpex2D.Framework.Math.Vector2;

namespace Sharpex2D.Framework.Rendering.DirectX10
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DirectXHelper
    {
        /// <summary>
        ///     Gets or sets the RenderTarget.
        /// </summary>
        internal static RenderTarget RenderTarget { set; get; }

        /// <summary>
        ///     Sets or gets the Direct2DFactory.
        /// </summary>
        internal static Factory Direct2DFactory { set; get; }

        internal static SlimDX.DirectWrite.Factory DirectWriteFactory { set; get; }

        /// <summary>
        ///     Converts the Color.
        /// </summary>
        /// <param name="value">The Color.</param>
        /// <returns>Color4.</returns>
        public static Color4 ConvertColor(Color value)
        {
            return new Color4(System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B));
        }

        /// <summary>
        ///     Converts the Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Rectangle.</returns>
        internal static Rectangle ConvertRectangle(Math.Rectangle rectangle)
        {
            return new Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width, (int) rectangle.Height);
        }

        /// <summary>
        ///     Converts the Vector2.
        /// </summary>
        /// <param name="target">The Vector.</param>
        /// <returns>PointF.</returns>
        public static PointF ConvertPointF(Vector2 target)
        {
            return new PointF(target.X, target.Y);
        }

        /// <summary>
        ///     Converts the Rectangle.
        /// </summary>
        /// <param name="ellipse">The Ellipse.</param>
        /// <returns>Ellipse.</returns>
        public static Ellipse ConvertEllipse(Math.Ellipse ellipse)
        {
            var ellipseX = new Ellipse
            {
                Center = ConvertPointF(ellipse.Position),
                RadiusX = ellipse.RadiusX,
                RadiusY = ellipse.RadiusY
            };

            return ellipseX;
        }

        /// <summary>
        ///     Converts the Vector2.
        /// </summary>
        /// <param name="p0">The Vector2.</param>
        /// <returns>PointF.</returns>
        public static PointF ConvertVector(Vector2 p0)
        {
            return new PointF(p0.X, p0.Y);
        }

        /// <summary>
        ///     Converts the SolidColorBrush.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>Brush.</returns>
        public static Brush ConvertSolidColorBrush(Color color)
        {
            return new SolidColorBrush(RenderTarget, ConvertColor(color));
        }

        /// <summary>
        ///     Converts the Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Rectangle.</returns>
        internal static RectangleF ConvertRectangleF(Math.Rectangle rectangle)
        {
            return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}