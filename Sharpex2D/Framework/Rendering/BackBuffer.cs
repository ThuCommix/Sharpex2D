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
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class BackBuffer
    {
        /// <summary>
        ///     Initializes a new BackBuffer class.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public BackBuffer(int width, int height)
            : this(new Vector2(width, height))
        {
        }

        /// <summary>
        ///     Initializes a new BackBuffer class.
        /// </summary>
        /// <param name="size">The Size.</param>
        public BackBuffer(Vector2 size)
        {
            if (size.X < 1 || size.Y < 1)
            {
                throw new InvalidOperationException("X and Y must be greater than 0.");
            }

            Width = (int) size.X;
            Height = (int) size.Y;
        }

        /// <summary>
        ///     Sets or gets the Scaling Value.
        /// </summary>
        public bool Scaling { set; get; }

        /// <summary>
        ///     Gets the Width.
        /// </summary>
        public int Width { private set; get; }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        public int Height { private set; get; }
    }
}