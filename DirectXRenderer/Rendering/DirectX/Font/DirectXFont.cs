using System;
using SharpDX.DirectWrite;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering.Font;

namespace SharpexGL.Framework.Rendering.DirectX.Font
{
    public class DirectXFont : IFont
    {
        #region IFont Implementation
        /// <summary>
        /// Measures the text.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <returns>Vector2</returns>
        public Vector2 MeasureString(string text)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sets or gets the Typeface.
        /// </summary>
        public Typeface Typeface { get; set; }

        #endregion

        private TextFormat _textFormat;

        /// <summary>
        /// Initializes a new DirectXFont.
        /// </summary>
        /// <param name="typeface">The Typeface</param>
        public DirectXFont(Typeface typeface)
        {
            Typeface = typeface;
            _textFormat = new TextFormat(DirectXHelper.DirectWriteFactory, typeface.FamilyName, GetWeightFromTypeface(),
                GetFontStyleFromTypeface(), typeface.Size);
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
