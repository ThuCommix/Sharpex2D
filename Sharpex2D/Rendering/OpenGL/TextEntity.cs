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

using Sharpex2D.Common.Extensions;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class TextEntity
    {
        /// <summary>
        /// Gets the Id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the Texture.
        /// </summary>
        public OpenGLTexture Texture { get; private set; }

        private readonly string _text;
        private readonly System.Drawing.Font _font;
        private readonly Color _color;

        /// <summary>
        /// Initializes a new TextEntity class.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="color">The Color.</param>
        /// <param name="wrapWidth">The Wordwrap Width.</param>
        public TextEntity(string text, OpenGLFont font, Color color, int wrapWidth = 0)
        {
            if (wrapWidth > 0) text = text.WordWrap(wrapWidth);
            var gdiFont = OpenGLHelper.ConvertFont(font);
            Id = text.GetHashCode() + gdiFont.GetHashCode() + color.GetHashCode();
            _text = text;
            _font = gdiFont;
            _color = color;
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        public void DrawText()
        {
            Texture = new OpenGLTexture(BitmapFont.DrawTextToBitmap(_text, _font, _color));
        }
    }
}
