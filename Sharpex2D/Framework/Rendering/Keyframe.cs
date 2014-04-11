
namespace Sharpex2D.Framework.Rendering
{
    public class Keyframe
    {
        /// <summary>
        /// Initializes a new Keyframe class.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public Keyframe(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Gets the X-Coord.
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Gets the Y-Coord.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; private set; }
    }
}
