using SharpDX.Direct2D1;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Rendering.DirectX11
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("DirectX11 Pen")]
    public class DirectXPen : IPen
    {
        #region IPen Implementation

        /// <summary>
        ///     Sets or gets the Size of the Pen.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        ///     Sets or gets the Color of the Pen.
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _colorBrush.Color = DirectXHelper.ConvertColor(value);
            }
        }

        #endregion

        private readonly SolidColorBrush _colorBrush;
        private Color _color;

        /// <summary>
        ///     Initializes a new DirectXPen class.
        /// </summary>
        public DirectXPen()
        {
            _colorBrush = new SolidColorBrush(DirectXHelper.RenderTarget, SharpDX.Color.Black);
            Color = Color.Black;
            Width = 1;
        }

        /// <summary>
        ///     Initializes a new DirectXPen class.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="width">The Width.</param>
        public DirectXPen(Color color, float width)
        {
            _colorBrush = new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color));
            Width = width;
        }

        /// <summary>
        ///     Gets the Pen.
        /// </summary>
        /// <returns>SolidColorBrush</returns>
        internal SolidColorBrush GetPen()
        {
            return _colorBrush;
        }
    }
}