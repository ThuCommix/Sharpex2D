using SharpexGL.Framework.Events;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Input.Events
{
    public class MouseLocationChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new MouseLocationChanged Event.
        /// </summary>
        /// <param name="location"></param>
        public MouseLocationChangedEvent(Vector2 location)
        {
            Location = location;
        }

        /// <summary>
        /// Gets the Location of the Mouse.
        /// </summary>
        public Vector2 Location { get; private set; }
    }
}
