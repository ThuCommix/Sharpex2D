
namespace SharpexGL.Framework.Rendering
{
    public interface IPen
    {
        /// <summary>
        /// Sets or gets the Size of the Pen.
        /// </summary>
        float Width { set; get; }
        /// <summary>
        /// Sets or gets the Color of the Pen.
        /// </summary>
        Color Color { set; get; }
    }
}
