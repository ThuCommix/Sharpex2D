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

using Sharpex2D.Content.Pipeline;
using SlimDX.DirectWrite;

namespace Sharpex2D.Rendering.DirectX10
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Content("DirectX10 Font")]
    public class DirectXFont : IFont
    {
        #region IFont Implementation

        /// <summary>
        /// Sets or gets the Typeface.
        /// </summary>
        public Typeface Typeface { get; set; }

        #endregion

        private readonly TextFormat _textFormat;

        /// <summary>
        /// Initializes a new DirectXFont.
        /// </summary>
        /// <param name="typeface">The Typeface</param>
        public DirectXFont(Typeface typeface)
        {
            Typeface = typeface;
            _textFormat = new TextFormat(DirectXHelper.DirectWriteFactory, typeface.FamilyName, GetWeightFromTypeface(),
                GetFontStyleFromTypeface(), FontStretch.Normal, typeface.Size, "");
        }

        /// <summary>
        /// Converts the Typeface style into a FontWeight.
        /// </summary>
        /// <returns>FontWeight</returns>
        private FontWeight GetWeightFromTypeface()
        {
            var weight = FontWeight.Normal;
            if (Typeface.Style == TypefaceStyle.Bold)
            {
                weight = FontWeight.Bold;
            }
            return weight;
        }

        /// <summary>
        /// Converts the Typeface style into a FontStyle.
        /// </summary>
        /// <returns>FontStyle</returns>
        private FontStyle GetFontStyleFromTypeface()
        {
            var fontStyle = FontStyle.Normal;

            if (Typeface.Style == TypefaceStyle.Italic)
            {
                fontStyle = FontStyle.Italic;
            }

            return fontStyle;
        }

        /// <summary>
        /// Gets the Font.
        /// </summary>
        /// <returns>TextFormat.</returns>
        public TextFormat GetFont()
        {
            return _textFormat;
        }
    }
}