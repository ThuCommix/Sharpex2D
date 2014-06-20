using System;

namespace Sharpex2D.Framework.Rendering.Fonts
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Typeface
    {
        private float _fontSize;

        /// <summary>
        ///     Initializes a new Typeface class.
        /// </summary>
        public Typeface()
        {
            FamilyName = "System";
            Size = 8;
            Style = TypefaceStyle.Regular;
        }

        /// <summary>
        ///     Sets or gets the FontFamily.
        /// </summary>
        public string FamilyName { set; get; }

        /// <summary>
        ///     Sets or gets the FontSize.
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
                    throw new ArgumentOutOfRangeException("value");
                }
            }
            get { return _fontSize; }
        }

        /// <summary>
        ///     Sets or gets the FontStyle.
        /// </summary>
        public TypefaceStyle Style { set; get; }
    }
}