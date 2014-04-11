using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Rendering
{
    public class DisplayMode
    {
        public DisplayMode(Vector2 size)
        {
            Width = (int)size.X;
            Height = (int)size.Y;
        }

        public DisplayMode(int width, int height)
            : this(new Vector2(width, height))
        {

        }

        /// <summary>
        /// Sets or gets the Scaling Value.
        /// </summary>
        public bool Scaling { set; get; }
        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { private set; get; }
        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { private set; get; }
    }
}
