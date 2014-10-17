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
    [Content("Graphics Device Interface Font")]
    public class GDIFont : IFont
    {
        #region IFont Implementation

        /// <summary>
        /// Sets or gets the Typeface.
        /// </summary>
        public Typeface Typeface { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new GdiFont class.
        /// </summary>
        /// <param name="familyName">The FamilyName.</param>
        /// <param name="size">The Size.</param>
        /// <param name="style">The Style.</param>
        public GDIFont(string familyName, float size, TypefaceStyle style)
        {
            Typeface = new Typeface {FamilyName = familyName, Size = size, Style = style};
        }

        /// <summary>
        /// Initializes a new GdiFont class.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        public GDIFont(Typeface typeface)
        {
            if (typeface == null) throw new ArgumentNullException("typeface");

            Typeface = typeface;
        }

        /// <summary>
        /// Gets the Font.
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Font GetFont()
        {
            return GetFont(Typeface);
        }

        /// <summary>
        /// Gets the Font.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        /// <returns>Font</returns>
        private static System.Drawing.Font GetFont(Typeface typeface)
        {
            return new System.Drawing.Font(typeface.FamilyName, typeface.Size, GetFontStyle(typeface.Style),
                GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Gets the FontStyle.
        /// </summary>
        /// <param name="style">The TypefaceStyle.</param>
        /// <returns>FontStyle</returns>
        private static FontStyle GetFontStyle(TypefaceStyle style)
        {
            switch (style)
            {
                case TypefaceStyle.Regular:
                    return FontStyle.Regular;
                case TypefaceStyle.Bold:
                    return FontStyle.Bold;
                case TypefaceStyle.Italic:
                    return FontStyle.Italic;
                case TypefaceStyle.Underline:
                    return FontStyle.Underline;
                case TypefaceStyle.Strikeout:
                    return FontStyle.Strikeout;
            }

            return FontStyle.Regular;
        }
    }
}