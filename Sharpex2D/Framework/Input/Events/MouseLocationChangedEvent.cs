using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Input.Events
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
