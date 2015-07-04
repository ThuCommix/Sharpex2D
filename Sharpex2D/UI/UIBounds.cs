// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Framework.UI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class UIBounds
    {
        /// <summary>
        /// Initializes a new UIBounds class.
        /// </summary>
        public UIBounds()
        {
            Height = 0;
            Width = 0;
        }

        /// <summary>
        /// Initializes a new UIBounds class.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public UIBounds(int x, int y, int width, int height)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Sets or gets the Width.
        /// </summary>
        public int Width { set; get; }

        /// <summary>
        /// Sets or gets the Height.
        /// </summary>
        public int Height { set; get; }

        /// <summary>
        /// Sets or gets the X-Coord.
        /// </summary>
        public int X { set; get; }

        /// <summary>
        /// Sets or gets the Y-Coord.
        /// </summary>
        public int Y { set; get; }
    }
}