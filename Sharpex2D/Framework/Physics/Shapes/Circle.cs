namespace Sharpex2D.Framework.Physics.Shapes
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Circle : IShape
    {
        /// <summary>
        ///     Initializes a new Circle class.
        /// </summary>
        public Circle()
        {
            Radius = 0;
        }

        /// <summary>
        ///     Initializes a new Circle class.
        /// </summary>
        /// <param name="radius">The Radius.</param>
        public Circle(float radius)
        {
            Radius = radius;
        }

        /// <summary>
        ///     Sets or gets the radius of the circle.
        /// </summary>
        public float Radius { set; get; }
    }
}