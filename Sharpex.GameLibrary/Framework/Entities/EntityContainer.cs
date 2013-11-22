using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.Entities
{
    public class EntityContainer
    {
        /// <summary>
        /// Initializes a new EntityContainer class.
        /// </summary>
        internal EntityContainer()
        {
            _entities = new List<Entity>();
        }

        private readonly List<Entity> _entities;

        /// <summary>
        /// Adds a new Entity to the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Add(Entity entity)
        {
            _entities.Add(entity);
        }
        /// <summary>
        /// Removes a Entity from the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Remove(Entity entity)
        {
            if (_entities.Contains(entity))
            {
                _entities.Remove(entity);
            }
        }
        /// <summary>
        /// Gets all Entities.
        /// </summary>
        /// <returns>Entity Array</returns>
        public Entity[] GetEntities()
        {
            return _entities.ToArray();
        }
        /// <summary>
        /// Gets a special Entity.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Entity</returns>
        public T Get<T>() where T : Entity
        {
            for (var i = 0; i <= _entities.Count - 1; i++)
            {
                if (_entities[i].GetType() == typeof (T))
                {
                    return (T) _entities[i];
                }
            }

            throw new InvalidOperationException("Entity not found (" + typeof(T).FullName + ").");
        }
    }
}
