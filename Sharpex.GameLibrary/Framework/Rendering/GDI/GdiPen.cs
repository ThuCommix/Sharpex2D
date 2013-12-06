using System.Drawing;

namespace SharpexGL.Framework.Rendering.GDI
{
    public class GdiPen : IPen
    {
        #region IPen Implementation

        /// <summary>
        /// Sets or gets the Size of the Pen.
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value;
                _pen.Width = value;
            }
        }

        /// <summary>
        /// Sets or gets the Color of the Pen.
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; _pen.Brush = new SolidBrush(value.ToWin32Color()); }
        }

        #endregion

        private readonly Pen _pen;
        private Color _color;
        private float _width;

        /// <summary>
        /// Initializes a new GdiPen class.
        /// </summary>
        public GdiPen()
        {
            _pen = new Pen(Brushes.Black);
            Color = Color.Black;
            Width = 1;
            _pen.Width = 1;
        }

        /// <summary>
        /// Gets the Pen.
        /// </summary>
        /// <returns>Pen</returns>
        internal Pen GetPen()
        {
            return _pen;
        }
    }
}
