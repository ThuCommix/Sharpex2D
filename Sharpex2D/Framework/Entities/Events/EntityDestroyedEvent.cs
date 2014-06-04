using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Entities.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class EntityDestroyedEvent : IEvent
    {
        /// <summary>
        ///     Initializes a new EntityDestroyedEvent class.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public EntityDestroyedEvent(Entity entity)
        {
            DestroyedEntity = entity;
        }

        /// <summary>
        ///     Gets the destroyed entity.
        /// </summary>
        public Entity DestroyedEntity { private set; get; }
    }
}