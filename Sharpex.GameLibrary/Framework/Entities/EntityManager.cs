using System;
using System.Collections.Generic;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Entities
{
    public class EntityManager
    {
        /// <summary>
        /// Initializes a new EntityManager class.
        /// </summary>
        public EntityManager()
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

            throw new InvalidOperationException("Entity not found (" + typeof (T).FullName + ").");
        }

        /// <summary>
        /// Gets a special Entity.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>Entity.</returns>
        public Entity GetEntityById(Guid id)
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
                    if (_entities.Contains(entity))
                    {
                        _entities.Remove(entity);
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
