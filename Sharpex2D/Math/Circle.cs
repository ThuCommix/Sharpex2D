// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace Sharpex2D.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public struct Circle : IGeometry
    {
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
        /// Gets or sets the Circle radius.
        /// </summary>
        public float Radius { set; get; }

        /// <summary>
        /// Gets or sets the center point of the Circle.
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// Gets the Distance between two circles.
        /// </summary>
        /// <param name="circle1">The first Circle.</param>
        /// <param name="circle2">The second Circle.</param>
        /// <returns>Distance</returns>
        public static float Distance(Circle circle1, Circle circle2)
        {
            return
                MathHelper.Sqrt(MathHelper.Pow((circle1.Position.X - circle2.Position.X), 2) +
                                MathHelper.Pow((circle1.Position.Y - circle2.Position.Y), 2));
        }

        /// <summary>
        /// Gets the Distance between this, and a second Circle.
        /// </summary>
        /// <param name="circle">The second Circle.</param>
        /// <returns>Distance</returns>
        public float Distance(Circle circle)
        {
            return
                MathHelper.Sqrt(MathHelper.Pow((Position.X - circle.Position.X), 2) +
                                MathHelper.Pow((Position.Y - circle.Position.Y), 2));
        }

        /// <summary>
        /// Indicates whether the Circle intersects with another Circle.
        /// </summary>
        /// <param name="circle">The second Circle.</param>
        /// <returns>True on intersect</returns>
        public bool IntersectsWith(Circle circle)
        {
            float r = Radius + circle.Radius;
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
            float r = circle1.Radius + circle2.Radius;
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