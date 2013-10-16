using SharpexGL.Framework.Components;
using SharpexGL.Framework.Math;
using Circle = SharpexGL.Framework.Physics.Shapes.Circle;
using Rectangle = SharpexGL.Framework.Physics.Shapes.Rectangle;

namespace SharpexGL.Framework.Physics.Collision
{
    public class CollisionManager : ICollision, IComponent
    {
        #region ICollision Implementation
        /// <summary>
        /// Indicates whether the particles intersect with each other.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        public bool IsIntersecting(Particle particle1, Particle particle2)
        {
            return InternalIsIntersecting(particle1, particle2);
        }
        #endregion

        #region CollisionManager Internal
        /// <summary>
        /// Initializes a new CollisionManager class.
        /// </summary>
        public CollisionManager()
        {
            SGL.Components.AddComponent(this);
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~CollisionManager()
        {
            SGL.Components.RemoveComponent(this);
        }

        /// <summary>
        /// Indicates whether the particles intersect with each other.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        private bool InternalIsIntersecting(Particle particle1, Particle particle2)
        {
            //Check out the particles base types, if one of them is not a known shape throw exception
            //Check particle 1
            if (!(particle1.Shape is Circle || particle1.Shape is Rectangle))
            {
                throw new UnknownShapeException("Unknown shape in " + particle1.GetType().Name);
            }
            //Check particle 2
            if (!(particle2.Shape is Circle || particle2.Shape is Rectangle))
            {
                throw new UnknownShapeException("Unknown shape in " + particle2.GetType().Name);
            }

            // Particle shapes are correct, continue

            //Check if both particles are rectangles
            if (particle1.Shape is Rectangle && particle2.Shape is Rectangle)
            {
                return RectangleIntersectsRectangle(particle1, particle2);
            }

            //Check if both particles are circles
            if (particle2.Shape is Circle && particle1.Shape is Circle)
            {
                return CircleIntersectsCircle(particle1, particle2);
            }

            //Check if the second particle is a circle
            if (particle1.Shape is Rectangle)
            {
                return RectangleIntersectsCircle(particle1, particle2);
            }

            //check if the second particle is a rectangle
            return RectangleIntersectsCircle(particle2, particle1);
        }

        /// <summary>
        /// Indicates whether the rectangle intersects with another rectangle.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True on intersecting</returns>
        private bool RectangleIntersectsRectangle(Particle particle1, Particle particle2)
        {
            /*/RectA.X1 < RectB.X2 && RectA.X2 > RectB.X1 &&
               RectA.Y1 < RectB.Y2 && RectA.Y2 > RectB.Y1/*/

            var rect1 = (Rectangle) particle1.Shape;
            var rect2 = (Rectangle)particle2.Shape;

            return particle1.Position.X < (particle2.Position.X + rect2.Width) &&
                   (particle1.Position.X + rect1.Width) > particle2.Position.X &&
                   particle1.Position.Y < (particle2.Position.Y + rect2.Height) &&
                   (particle1.Position.Y + rect1.Height) > particle2.Position.Y;

        }
        /// <summary>
        /// Indicates whether the rectangle intersects with a circle.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True on intersecting</returns>
        private bool RectangleIntersectsCircle(Particle particle1, Particle particle2)
        {
            var rect = (Rectangle) particle1.Shape;
            var circle = (Circle) particle2.Shape;

            var circleDistance = Vector2.Abs(particle2.Position - new Vector2(particle1.Position.X + rect.Width * 0.5f, particle1.Position.Y + rect.Height * 0.5f));
            var boxSize = new Vector2(rect.Width, rect.Height) / 2f;

            if (circleDistance.X > boxSize.X + circle.Radius ||
                circleDistance.Y > boxSize.Y + circle.Radius)
                return false;

            if (circleDistance.X <= boxSize.X ||
                circleDistance.Y <= boxSize.Y)
                return true;

            return (circleDistance - boxSize).LengthSquared <= circle.Radius * circle.Radius;
        }
        /// <summary>
        /// Indicates whether the circle intersects with a circle.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True on intersecting</returns>
        private bool CircleIntersectsCircle(Particle particle1, Particle particle2)
        {
            var circle1 = (Circle) particle1.Shape;
            var circle2 = (Circle) particle2.Shape;

            return (particle1.Position - particle2.Position).Length <
                   (circle1.Radius + circle2.Radius);
        }

        #endregion
    }
}
