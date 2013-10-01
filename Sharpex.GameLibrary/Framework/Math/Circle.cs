
namespace SharpexGL.Framework.Math
{
    public struct Circle
    {
        /// <summary>
        /// Gets or sets the Circle radius.
        /// </summary>
        public float Radius { set; get; }

        /// <summary>
        /// Gets or sets the center point of the Circle.
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// Creates a new Circle.
        /// </summary>
        /// <param name="position">The Position.</param>
        /// <param name="radius">The Radius.</param>
        public Circle(Vector2 position, float radius) : this()
        {
            Position = position;
            Radius = radius;
        }

        /// <summary>
        /// Gets the Distance between two circles.
        /// </summary>
        /// <param name="circle1">The first Circle.</param>
        /// <param name="circle2">The second Circle.</param>
        /// <returns>Distance</returns>
        public static float Distance(Circle circle1, Circle circle2)
        {
            return MathHelper.Sqrt(MathHelper.Pow((circle1.Position.X - circle2.Position.X), 2) + MathHelper.Pow((circle1.Position.Y - circle2.Position.Y), 2));
        }

        /// <summary>
        /// Gets the Distance between this, and a second Circle.
        /// </summary>
        /// <param name="circle">The second Circle.</param>
        /// <returns>Distance</returns>
        public float Distance(Circle circle)
        {
            return MathHelper.Sqrt(MathHelper.Pow((Position.X - circle.Position.X), 2) + MathHelper.Pow((Position.Y - circle.Position.Y), 2));
        }

        /// <summary>
        /// Indicates whether the Circle intersects with another Circle.
        /// </summary>
        /// <param name="circle">The second Circle.</param>
        /// <returns>True on intersect</returns>
        public bool IntersectsWith(Circle circle)
        {
            var r = Radius + circle.Radius;
            r *= r;
            return r <
                   MathHelper.Pow((Position.X + circle.Position.X), 2) +
                   MathHelper.Pow((Position.Y + circle.Position.Y), 2);
        }

        /// <summary>
        /// Indicates whether the first Circle intersects with the second Circle.
        /// </summary>
        /// <param name="circle1">The first Circle.</param>
        /// <param name="circle2">The second Circle.</param>
        /// <returns>True on intersect</returns>
        public static bool Intersects(Circle circle1, Circle circle2)
        {
            var r = circle1.Radius + circle2.Radius;
            r *= r;
            return r <
                   MathHelper.Pow((circle1.Position.X + circle2.Position.X), 2) +
                   MathHelper.Pow((circle1.Position.Y + circle2.Position.Y), 2);
        }
        /// <summary>
        /// Converts the circle in to a string.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return Position.X + ";" + Position.Y + ";" + Radius;
        }
    }
}
