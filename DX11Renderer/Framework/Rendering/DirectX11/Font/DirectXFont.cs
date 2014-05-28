using SharpDX.DirectWrite;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Rendering.Font;

namespace Sharpex2D.Framework.Rendering.DirectX11.Font
{
    public class DirectXFont : IFont, IContent
    {
        #region IFont Implementation
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
