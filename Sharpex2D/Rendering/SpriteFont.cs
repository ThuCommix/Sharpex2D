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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    public class SpriteFont : IContent
    {
        public enum FontStyle
        {
            /// <summary>
            /// Regular text style.
            /// </summary>
            Regular,

            /// <summary>
            /// Bold text style.
            /// </summary>
            Bold,

            /// <summary>
            /// Italic text style.
            /// </summary>
            Italic
        }

        private readonly Dictionary<char, Rectangle> _charLayout;
        private readonly Texture2D _fontTexture;

        /// <summary>
        /// Initializes a new SpriteFont class.
        /// </summary>
        /// <param name="fontName">The FontName.</param>
        /// <param name="size">The Size.</param>
        /// <param name="spacing">The Spacing.</param>
        /// <param name="whiteSpacing">The WhiteSpacing.</param>
        /// <param name="fontStyle">The FontStyle.</param>
        /// <param name="fontTexture">The FontTexture.</param>
        /// <param name="charLayout">The CharLayout.</param>
        internal SpriteFont(string fontName, float size, float spacing, float whiteSpacing, FontStyle fontStyle,
            Texture2D fontTexture, Dictionary<char, Rectangle> charLayout)
        {
            FontName = fontName;
            Size = size;
            Spacing = spacing;
            WhiteSpacing = whiteSpacing;
            Style = fontStyle;
            _fontTexture = fontTexture;
            _charLayout = charLayout;
        }

        /// <summary>
        /// Gets the font name.
        /// </summary>
        public string FontName { private set; get; }

        /// <summary>
        /// Gets the font size.
        /// </summary>
        public float Size { private set; get; }

        /// <summary>
        /// Gets the spacing.
        /// </summary>
        public float Spacing { private set; get; }

        /// <summary>
        /// Gets the white spacing size
        /// </summary>
        public float WhiteSpacing { private set; get; }

        /// <summary>
        /// Gets the font style.
        /// </summary>
        public FontStyle Style { private set; get; }

        /// <summary>
        /// Gets or sets the default replace char
        /// </summary>
        public char DefaultReplaceChar { set; get; } = '_';

        /// <summary>
        /// Replaces all illegal characters.
        /// </summary>
        /// <param name="value">The String.</param>
        /// <param name="replacement">The Replacement.</param>
        /// <returns>String.</returns>
        public string ReplaceIllegalChars(string value, char replacement)
        {
            var sb = new StringBuilder(value);
            for (int i = 0; i < value.Length; i++)
            {
                if (!_charLayout.ContainsKey(value[i]))
                {
                    sb[i] = replacement;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text)
        {
            float offsetWidth = 0;
            float offsetHeight = 0;
            float charHeight = 0;
            float maxWidth = 0;
            const float heightPadding = 2f;

            string[] lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (string line in lines)
            {
                foreach (char character in line)
                {
                    Rectangle charLayout = _charLayout[character];
                    offsetWidth += charLayout.Width;
                    charHeight = charLayout.Height;
                }

                if (maxWidth < offsetWidth)
                    maxWidth = offsetWidth;

                offsetHeight += charHeight + heightPadding;
                offsetWidth = 0;
            }

            return new Vector2(maxWidth, offsetHeight == 0 ? offsetHeight + charHeight + heightPadding : offsetHeight);
        }

        /// <summary>
        /// Draws a text.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="text">The Text.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        internal void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            DrawText(spriteBatch, text, new Rectangle(position.X, position.Y, 0, 0), TextFormat.None, color);
        }

        /// <summary>
        /// Draws a text.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="text">The Text.</param>
        /// <param name="layoutRectangle">The LayoutRectangle.</param>
        /// <param name="format">The TextFormat.</param>
        /// <param name="color">The Color.</param>
        internal void DrawText(SpriteBatch spriteBatch, string text, Rectangle layoutRectangle, TextFormat format,
            Color color)
        {
            float offsetWidth = 0;
            float offsetHeight = 0;
            float charHeight = 0;
            const float heightPadding = 2f;

            text = ReplaceIllegalChars(text, DefaultReplaceChar);

            if (format == TextFormat.None)
            {
                string[] lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    foreach (char character in line)
                    {
                        Rectangle charLayout = _charLayout[character];
                        spriteBatch.DrawTexture(_fontTexture, charLayout,
                            new Rectangle(layoutRectangle.X + offsetWidth, layoutRectangle.Y + offsetHeight,
                                charLayout.Width, charLayout.Height),
                            color);
                        offsetWidth += charLayout.Width;
                        charHeight = charLayout.Height;
                    }
                    offsetHeight += charHeight + heightPadding;
                    offsetWidth = 0;
                }
            }
            else if (format == TextFormat.Wrap)
            {
                string[] lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    foreach (char character in line)
                    {
                        Rectangle charLayout = _charLayout[character];
                        if (offsetWidth + charLayout.Width > layoutRectangle.Width)
                        {
                            offsetHeight += charLayout.Height + heightPadding;
                            offsetWidth = 0;
                        }

                        if (offsetHeight > layoutRectangle.Height)
                            return;

                        spriteBatch.DrawTexture(_fontTexture, charLayout,
                            new Rectangle(layoutRectangle.X + offsetWidth, layoutRectangle.Y + offsetHeight,
                                charLayout.Width, charLayout.Height),
                            color);
                        offsetWidth += charLayout.Width;
                        charHeight = charLayout.Height;
                    }
                    offsetHeight += charHeight + heightPadding;
                    offsetWidth = 0;
                }
            }
            else
            {
                string[] lines = text.Split(new[] {Environment.NewLine, " ", "\t"}, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    float charWidth = line.Sum(character => _charLayout[character].Width);

                    if (offsetWidth + charWidth > layoutRectangle.Width)
                    {
                        offsetHeight += _charLayout[line[0]].Height + heightPadding;
                        offsetWidth = 0;
                    }

                    if (offsetHeight > layoutRectangle.Height)
                        return;
                    foreach (char character in line)
                    {
                        Rectangle charLayout = _charLayout[character];
                        spriteBatch.DrawTexture(_fontTexture, charLayout,
                            new Rectangle(layoutRectangle.X + offsetWidth, layoutRectangle.Y + offsetHeight,
                                charLayout.Width, charLayout.Height),
                            color);
                        offsetWidth += charLayout.Width;
                    }
                    offsetWidth += _charLayout[' '].Width;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the string contains illegal characters.
        /// </summary>
        /// <param name="value">The String.</param>
        /// <returns>True if the string cotains an illegal character.</returns>
        public bool IsIllegalString(string value)
        {
            return value.Any(IsIllegalChar);
        }

        /// <summary>
        /// Gets a value indicating whether the character is a illegal character.
        /// </summary>
        /// <param name="character">The Character.</param>
        /// <returns>True if the character is illegal.</returns>
        public bool IsIllegalChar(char character)
        {
            return !_charLayout.ContainsKey(character);
        }
    }
}
