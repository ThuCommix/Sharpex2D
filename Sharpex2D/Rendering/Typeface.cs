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

namespace Sharpex2D.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Typeface
    {
        private float _fontSize;

        /// <summary>
        ///     Initializes a new Typeface class.
        /// </summary>
        public Typeface()
        {
            FamilyName = "System";
            Size = 8;
            Style = TypefaceStyle.Regular;
        }

        /// <summary>
        ///     Sets or gets the FontFamily.
        /// </summary>
        public string FamilyName { set; get; }

        /// <summary>
        ///     Sets or gets the FontSize.
        /// </summary>
        public float Size
        {
            set
            {
                if (value > 0)
                {
                    _fontSize =
                        value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value");
                }
            }
            get { return _fontSize; }
        }

        /// <summary>
        ///     Sets or gets the FontStyle.
        /// </summary>
        public TypefaceStyle Style { set; get; }
    }
}