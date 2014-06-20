using System.Drawing;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Rendering.Fonts;
using Font = SlimDX.Direct3D9.Font;

namespace Sharpex2D.Framework.Rendering.DirectX9.Fonts
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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