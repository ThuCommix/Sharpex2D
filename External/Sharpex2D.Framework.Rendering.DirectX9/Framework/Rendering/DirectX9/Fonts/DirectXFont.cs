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

using System.Drawing;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Rendering.Fonts;
using Font = SlimDX.Direct3D9.Font;

namespace Sharpex2D.Framework.Rendering.DirectX9.Fonts
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Content("DirectX9 Font")]
    public class DirectXFont : IFont
    {
        #region IFont Implementation

        /// <summary>
        ///     Sets or gets the Typeface.
        /// </summary>
        public Typeface Typeface { get; set; }

        #endregion

        private readonly Font _font;

        /// <summary>
        ///     Initializes a new DirectXFont.
        /// </summary>
        /// <param name="typeface">The Typeface</param>
        public DirectXFont(Typeface typeface)
        {
            _font = new Font(DirectXHelper.Direct3D9, ConvertTypefaceToFont(typeface));
        }

        /// <summary>
        ///     Converts
        /// </summary>
        /// <param name="typeface"></param>
        /// <returns></returns>
        private System.Drawing.Font ConvertTypefaceToFont(Typeface typeface)
        {
            switch (typeface.Style)
            {
                case TypefaceStyle.Bold:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, FontStyle.Bold);
                case TypefaceStyle.Italic:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, FontStyle.Italic);
                case TypefaceStyle.Regular:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, FontStyle.Regular);
                case TypefaceStyle.Strikeout:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, FontStyle.Strikeout);
                case TypefaceStyle.Underline:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, FontStyle.Underline);
                default:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, FontStyle.Regular);
            }
        }

        /// <summary>
        ///     Gets the Font.
        /// </summary>
        /// <returns>TextFormat.</returns>
        public Font GetFont()
        {
            return _font;
        }
    }
}