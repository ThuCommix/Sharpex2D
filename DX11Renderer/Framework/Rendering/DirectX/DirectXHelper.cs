using System;
using SharpDX;
using SharpDX.Direct2D1;

namespace Sharpex2D.Framework.Rendering.DirectX
{
    internal static class DirectXHelper
    {
        /// <summary>
        /// Sets or gets the RenderTarget.
        /// </summary>
        public static RenderTarget RenderTarget { set; get; }
        /// <summary>
        /// Sets or gets the D2DFactory.
        /// </summary>
        public static Factory D2DFactory { set; get; }
        /// <summary>
        /// Sets or gets the DirectWriteFactory.
        /// </summary>
        public static SharpDX.DirectWrite.Factory DirectWriteFactory { set; get; }
        /// <summary>
        /// Converts a Rectangle into DxRectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>DxRectangle.</returns>
        public static RectangleF ConvertRectangle(Math.Rectangle rectangle)
        {
            return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
        /// <summary>
        /// Converts a Color into DxColor.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>DxColor.</returns>
        public static SharpDX.Color ConvertColor(Color color)
        {
            return new SharpDX.Color(color.R, color.G, color.B, color.A);
        }
        /// <summary>
        /// Converts a Color into a SolidColorBrush.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>SolidColorBrush</returns>
        public static SolidColorBrush ConvertSolidColorBrush(Color color)
        {
            return new SolidColorBrush(RenderTarget, ConvertColor(color));
        }
        /// <summary>
        /// Converts a Vector into DxVector.
        /// </summary>
        /// <param name="vector">The Vector.</param>
        /// <returns>DxVector</returns>
        public static Vector2 ConvertVector(Math.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        /// <summary>
        /// Converts a Rectangle into a Ellipse.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Ellipse</returns>
        public static Ellipse ConvertEllipse(Math.Rectangle rectangle)
        {
            var ellipse = new Ellipse
            {
                Point = ConvertVector(rectangle.Center),
                RadiusX = rectangle.Width/2,
                RadiusY = rectangle.Height/2
            };
            return ellipse;
        }
    }
}
