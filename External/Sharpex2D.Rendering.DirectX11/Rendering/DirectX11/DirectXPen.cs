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

using SharpDX.Direct2D1;
using Sharpex2D.Content.Pipeline;

namespace Sharpex2D.Rendering.DirectX11
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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