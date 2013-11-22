using SharpexGL.Framework.Events;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Entities.Events
{
    public class EntityPositionChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new PositionChangedEvent class.
        /// </summary>
        /// <param name="delta">The Delta.</param>
        public EntityPositionChangedEvent(Vector2 delta)
        {
            Delta = delta;
        }

        /// <summary>
        /// Gets the Delta.
        /// </summary>
        public Vector2 Delta { private set; get; }
    }
}
