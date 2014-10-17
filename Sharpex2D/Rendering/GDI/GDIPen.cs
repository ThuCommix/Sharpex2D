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

using System;
using System.Drawing;
using Sharpex2D.Content.Pipeline;

namespace Sharpex2D.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Content("Graphics Device Interface Pen")]
    public class GDIPen : IPen, IDisposable
    {
        #region IPen Implementation

        /// <summary>
        /// Sets or gets the Size of the Pen.
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
        /// Sets or gets the Color of the Pen.
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _pen.Brush = new SolidBrush(GDIHelper.ConvertColor(value));
            }
        }

        #endregion

        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
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

        private readonly System.Drawing.Pen _pen;
        private Color _color;
        private float _width;

        /// <summary>
        /// Initializes a new GdiPen class.
        /// </summary>
        public GDIPen()
        {
            _pen = new System.Drawing.Pen(Brushes.Black);
            Color = Color.Black;
            Width = 1;
            _pen.Width = 1;
        }

        /// <summary>
        /// Initializes a new GdiPen class.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="width">The Width.</param>
        public GDIPen(Color color, float width)
        {
            _pen = new System.Drawing.Pen(new SolidBrush(GDIHelper.ConvertColor(color)), width);
        }

        /// <summary>
        /// Gets the Pen.
        /// </summary>
        /// <returns>Pen</returns>
        internal System.Drawing.Pen GetPen()
        {
            return _pen;
        }
    }
}