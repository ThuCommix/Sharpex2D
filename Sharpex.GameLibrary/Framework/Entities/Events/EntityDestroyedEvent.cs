using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Entities.Events
{
    public class EntityDestroyedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new EntityDestroyedEvent class.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public EntityDestroyedEvent(Entity entity)
        {
            DestroyedEntity = entity;
        }

        /// <summary>
        /// Gets the destroyed entity.
        /// </summary>
        public Entity DestroyedEntity { private set; get; }
    }
}
