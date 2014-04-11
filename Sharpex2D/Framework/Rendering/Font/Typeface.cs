using System;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Factory;

namespace Sharpex2D.Framework.Rendering.Font
{
    public class Typeface : IContent
    {

        /// <summary>
        /// Gets the Factory.
        /// </summary>
        public static TypefaceFactory Factory { private set; get; }

        private float _fontSize;

        /// <summary>
        /// Initializes a new Typeface class.
        /// </summary>
        public Typeface()
        {
            FamilyName = "System";
            Size = 8;
            Style = TypefaceStyle.Regular;
        }

        /// <summary>
        /// Initializes a new Typeface class.
        /// </summary>
        static Typeface()
        {
            Factory = new TypefaceFactory();
        }

        /// <summary>
        /// Sets or gets the FontFamily.
        /// </summary>
        public string FamilyName { set; get; }

        /// <summary>
        /// Sets or gets the FontSize.
        /// </summary>
        public float Size
        {
            set
            {
                if (value > 0)
                {
                    _fontSize =
                        value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(paramName: "value");
                }
            }
            get
            {
                return _fontSize;
            }
        }

        /// <summary>
        /// Sets or gets the FontStyle.
        /// </summary>
        public TypefaceStyle Style { set; get; }
    }
}
