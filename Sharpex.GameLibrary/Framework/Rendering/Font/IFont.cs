using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Font
{
    public interface IFont
    {
        /// <summary>
        /// Measures the text.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <returns>Vector2</returns>
        Vector2 MeasureString(string text);

        /// <summary>
        /// Sets or gets the Typeface.
        /// </summary>
        Typeface Typeface { set; get; }
    }
}
