using System;

namespace Sharpex2D
{
    public class CopyrightAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new CopyrightAttribute class.
        /// </summary>
        /// <param name="copyright">The Copyright.</param>
        public CopyrightAttribute(string copyright)
        {
            Copyright = copyright;
        }

        /// <summary>
        ///     Gets the copyright.
        /// </summary>
        public string Copyright { private set; get; }
    }
}