using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Entities
{
    public class EntityEnvironment
    {
        /// <summary>
        /// Initializes a new EntityManager class.
        /// </summary>
        public EntityEnvironment()
        {
            _entities = new Dictionary<int, Entity>();
        }

        private readonly Dictionary<int, Entity> _entities;

        /// <summary>
        /// Adds a new Entity to the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Add(Entity entity)
        {
            if (!_entities.ContainsKey(entity.Id))
            {
                _entities.Add(entity.Id, entity);
            }
            else
            {
                throw new ArgumentException("The entity id is already in use.");
            }
        }

        /// <summary>
        /// Removes a Entity from the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Remove(Entity entity)
        {
            if (_entities.ContainsValue(entity))
            {
                _entities.Remove(entity.Id);
            }
        }

        /// <summary>
        /// Gets all Entities.
        /// </summary>
        /// <returns>Entity Array</returns>
        public Entity[] GetEntities()
        {
            var entities = new Entity[_entities.Values.Count];
            _entities.Values.CopyTo(entities, 0);
            return entities;
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

            throw new InvalidOperationException("Entity not found (" + typeof (T).FullName + ").");
        }

        /// <summary>
        /// Gets a special Entity.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>Entity.</returns>
        public Entity GetEntityById(int id)
        {
            for (var i = 0; i <= _entities.Count - 1; i++)
            {
                if (_entities[i].Id == id)
                {
                    return _entities[i];
                }
            }

            throw new InvalidOperationException("Entity not found (" + id + ").");
        }

        /// <summary>
        /// Clears all Entities.
        /// </summary>
        public void Clear()
        {
            _entities.Clear();
        }

        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (var entity in GetEntities())
            {
                if (entity.IsDestroyed)
                {
                    if (_entities.ContainsValue(entity))
                    {
                        _entities.Remove(entity.Id);
                    }
                }
                else
                {
                    entity.Tick(elapsed);
                }
            }
        }

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            foreach (var entity in GetEntities())
            {
                if (!entity.IsDestroyed)
                {
                    entity.Render(renderer, elapsed);
                }
            }
        }
    }
}
