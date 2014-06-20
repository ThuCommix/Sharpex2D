using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.UI;

namespace Sharpex2D.Framework.Common.Extensions
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class UIBoundsExtension
    {
        /// <summary>
        ///     Returns a new Rectangle.
        /// </summary>
        /// <param name="bounds">The UIBounds.</param>
        /// <returns>Rectangle</returns>
        public static Rectangle ToRectangle(this UIBounds bounds)
        {
            return new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}