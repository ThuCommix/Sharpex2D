
namespace Sharpex2D.Framework.Physics.Shapes
{
    public class Rectangle : IShape
    {
        /// <summary>
        /// Sets or gets the width of the rectangle.
        /// </summary>
        public float Width { set; get; }
        /// <summary>
        /// Sets or gets the height of the rectangle.
        /// </summary>
        public float Height { set; get; }
        /// <summary>
        /// Initializes a new Rectangle class.
        /// </summary>
        public Rectangle()
        {
            Width = 0;
            Height = 0;
        }
    }
}
