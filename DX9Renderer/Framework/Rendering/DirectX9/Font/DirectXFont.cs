using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Rendering.Font;

namespace Sharpex2D.Framework.Rendering.DirectX9.Font
{
    public class DirectXFont : IFont, IContent
    {
        #region IFont Implementation
        /// <summary>
        /// Sets or gets the Typeface.
        /// </summary>
        public Typeface Typeface { get; set; }

        #endregion

        private readonly SlimDX.Direct3D9.Font _font;

        /// <summary>
        /// Initializes a new DirectXFont.
        /// </summary>
        /// <param name="typeface">The Typeface</param>
        public DirectXFont(Typeface typeface)
        {
           _font = new SlimDX.Direct3D9.Font(DirectXHelper.Direct3D9, ConvertTypefaceToFont(typeface));
        }

        /// <summary>
        /// Converts 
        /// </summary>
        /// <param name="typeface"></param>
        /// <returns></returns>
        private System.Drawing.Font ConvertTypefaceToFont(Typeface typeface)
        {
            switch (typeface.Style)
            {
                case TypefaceStyle.Bold:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, System.Drawing.FontStyle.Bold);
                case TypefaceStyle.Italic:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, System.Drawing.FontStyle.Italic);
                case TypefaceStyle.Regular:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, System.Drawing.FontStyle.Regular);
                case TypefaceStyle.Strikeout:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, System.Drawing.FontStyle.Strikeout);
                case TypefaceStyle.Underline:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, System.Drawing.FontStyle.Underline);
                default:
                    return new System.Drawing.Font(typeface.FamilyName, typeface.Size, System.Drawing.FontStyle.Regular);
            }
        }

        /// <summary>
        /// Gets the Font.
        /// </summary>
        /// <returns>TextFormat.</returns>
        public SlimDX.Direct3D9.Font GetFont()
        {
            return _font;
        }
    }
}
