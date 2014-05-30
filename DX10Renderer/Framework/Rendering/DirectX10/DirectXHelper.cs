using System.Drawing;
using SlimDX;
using SlimDX.Direct2D;
using Brush = SlimDX.Direct2D.Brush;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;
using Vector2 = Sharpex2D.Framework.Math.Vector2;

namespace Sharpex2D.Framework.Rendering.DirectX10
{
    public class DirectXHelper
    {
        /// <summary>
        /// Gets or sets the RenderTarget.
        /// </summary>
        internal static RenderTarget RenderTarget { set; get; }
        /// <summary>
        /// Sets or gets the Direct2DFactory.
        /// </summary>
        internal static Factory Direct2DFactory { set; get; }

        internal static SlimDX.DirectWrite.Factory DirectWriteFactory { set; get; }
        /// <summary>
        /// Converts the Color.
        /// </summary>
        /// <param name="value">The Color.</param>
        /// <returns>Color4.</returns>
        public static Color4 ConvertColor(Color value)
        {
            return new Color4(System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B));
        }
        /// <summary>
        /// Converts the Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Rectangle.</returns>
        internal static System.Drawing.Rectangle ConvertRectangle(Rectangle rectangle)
        {
            return new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
        /// <summary>
        /// Converts the Vector2.
        /// </summary>
        /// <param name="target">The Vector.</param>
        /// <returns>PointF.</returns>
        public static PointF ConvertPointF(Vector2 target)
        {
            return new PointF(target.X, target.Y);
        }
        /// <summary>
        /// Converts the Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Ellipse.</returns>
        public static Ellipse ConvertEllipse(Rectangle rectangle)
        {
            var ellipse = new Ellipse
            {
                Center = ConvertPointF(rectangle.Center),
                RadiusX = rectangle.Width/2,
                RadiusY = rectangle.Height/2
            };

            return ellipse;
        }
        /// <summary>
        /// Converts the Vector2.
        /// </summary>
        /// <param name="p0">The Vector2.</param>
        /// <returns>PointF.</returns>
        public static PointF ConvertVector(Vector2 p0)
        {
            return new PointF(p0.X, p0.Y);
        }
        /// <summary>
        /// Converts the SolidColorBrush.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>Brush.</returns>
        public static Brush ConvertSolidColorBrush(Color color)
        {
            return new SolidColorBrush(RenderTarget, ConvertColor(color));
        }
    }
}
