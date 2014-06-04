using System;
using System.Drawing;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Rendering.Font;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Graphics Device Interface Font")]
    public class GdiFont : IFont
    {
        #region IFont Implementation

        /// <summary>
        ///     Sets or gets the Typeface.
        /// </summary>
        public Typeface Typeface { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new GdiFont class.
        /// </summary>
        /// <param name="familyName">The FamilyName.</param>
        /// <param name="size">The Size.</param>
        /// <param name="style">The Style.</param>
        public GdiFont(string familyName, float size, TypefaceStyle style)
        {
            Typeface = new Typeface {FamilyName = familyName, Size = size, Style = style};
        }

        /// <summary>
        ///     Initializes a new GdiFont class.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        public GdiFont(Typeface typeface)
        {
            if (typeface == null) throw new ArgumentNullException("typeface");

            Typeface = typeface;
        }

        /// <summary>
        ///     Gets the Font.
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Font GetFont()
        {
            return GetFont(Typeface);
        }

        /// <summary>
        ///     Gets the Font.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        /// <returns>Font</returns>
        private static System.Drawing.Font GetFont(Typeface typeface)
        {
            return new System.Drawing.Font(typeface.FamilyName, typeface.Size, GetFontStyle(typeface.Style),
                GraphicsUnit.Pixel);
        }

        /// <summary>
        ///     Gets the FontStyle.
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