using System;
using System.Drawing;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Graphics Device Interface Pen")]
    public class GdiPen : IPen, IDisposable
    {
        #region IPen Implementation

        /// <summary>
        ///     Sets or gets the Size of the Pen.
        /// </summary>
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _pen.Width = value;
            }
        }

        /// <summary>
        ///     Sets or gets the Color of the Pen.
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _pen.Brush = new SolidBrush(value.ToWin32Color());
            }
        }

        #endregion

        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    _pen.Dispose();
                }
            }
        }

        #endregion

        private readonly Pen _pen;
        private Color _color;
        private float _width;

        /// <summary>
        ///     Initializes a new GdiPen class.
        /// </summary>
        public GdiPen()
        {
            _pen = new Pen(Brushes.Black);
            Color = Color.Black;
            Width = 1;
            _pen.Width = 1;
        }

        /// <summary>
        ///     Initializes a new GdiPen class.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="width">The Width.</param>
        public GdiPen(Color color, float width)
        {
            _pen = new Pen(new SolidBrush(color.ToWin32Color()), width);
        }

        /// <summary>
        ///     Gets the Pen.
        /// </summary>
        /// <returns>Pen</returns>
        internal Pen GetPen()
        {
            return _pen;
        }
    }
}