using SharpDX;
using SharpDX.Direct2D1;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;

namespace Sharpex2D.Framework.Rendering.DirectX11
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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