namespace Sharpex2D.Framework.Physics.Shapes
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Rectangle : IShape
    {
        /// <summary>
        ///     Initializes a new Rectangle class.
        /// </summary>
        public Rectangle()
        {
            Width = 0;
            Height = 0;
        }

        /// <summary>
        ///     Sets or gets the width of the rectangle.
        /// </summary>
        public float Width { set; get; }

        /// <summary>
        ///     Sets or gets the height of the rectangle.
        /// </summary>
        public float Height { set; get; }
    }
}