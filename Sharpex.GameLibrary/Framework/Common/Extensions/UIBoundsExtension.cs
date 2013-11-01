using SharpexGL.Framework.Math;
using SharpexGL.Framework.UI;

namespace SharpexGL.Framework.Common.Extensions
{
    public static class UIBoundsExtension
    {
        /// <summary>
        /// Returns a new Rectangle.
        /// </summary>
        /// <param name="bounds">The UIBounds.</param>
        /// <returns>Rectangle</returns>
        public static Rectangle ToRectangle(this UIBounds bounds)
        {
            return new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}
