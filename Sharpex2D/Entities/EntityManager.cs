// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

using System;
using System.Collections;
using System.Collections.Generic;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Entities
{
    public class EntityManager : IList<Entity>, IUpdateable, IDrawable
    {
        private readonly List<Entity> _entities;

        /// <summary>
        /// Raises when a entity was added
        /// </summary>
        public event EventHandler<EntityChangedEventArgs> EntityAdded;

        /// <summary>
        /// Raises when a entity was removed
        /// </summary>
        public event EventHandler<EntityChangedEventArgs> EntityRemoved;

        /// <summary>
        /// Initializes a new EntityManager class
        /// </summary>
        public EntityManager()
        {
            _entities = new List<Entity>();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<Entity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="item">The Entity</param>
        public void Add(Entity item)
        {
            _entities.Add(item);
            EntityAdded?.Invoke(this, new EntityChangedEventArgs(item));
        }

        /// <summary>
        /// Clears the entity list
        /// </summary>
        public void Clear()
        {
            foreach (var entity in _entities)
            {
                Remove(entity);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the entity exists in the list
        /// </summary>
        /// <param name="item">The Entity</param>
        /// <returns>True on success</returns>
        public bool Contains(Entity item)
        {
            return _entities.Contains(item);
        }

        /// <summary>
        /// Copies an array of entities at the specified index
        /// </summary>
        /// <param name="array">The Array</param>
        /// <param name="arrayIndex">The Index</param>
        public void CopyTo(Entity[] array, int arrayIndex)
        {
            _entities.CopyTo(array, arrayIndex);

            foreach (var entity in array)
            {
                EntityAdded?.Invoke(this, new EntityChangedEventArgs(entity));
            }
        }

        /// <summary>
        /// Removes the entity
        /// </summary>
        /// <param name="item">The Entity</param>
        /// <returns>True on success</returns>
        public bool Remove(Entity item)
        {
            var result = _entities.Remove(item);
            if (result)
                EntityRemoved?.Invoke(this, new EntityChangedEventArgs(item));

            return result;
        }

        /// <summary>
        /// Gets the count
        /// </summary>
        public int Count => _entities.Count;

        /// <summary>
        /// A value indicating whether the list is read only
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the index of the specified entity
        /// </summary>
        /// <param name="item">The Entity</param>
        /// <returns>Index</returns>
        public int IndexOf(Entity item)
        {
            return _entities.IndexOf(item);
        }

        /// <summary>
        /// Inserts the entity at the specified index
        /// </summary>
        /// <param name="index">The Index</param>
        /// <param name="item">The Entity</param>
        public void Insert(int index, Entity item)
        {
            _entities.Insert(index, item);
            EntityAdded?.Invoke(this, new EntityChangedEventArgs(item));
        }

        /// <summary>
        /// Removes the entity at the specified index
        /// </summary>
        /// <param name="index">The Index</param>
        public void RemoveAt(int index)
        {
            var item = this[index];
            Remove(item);
        }

        /// <summary>
        /// Gets or sets the entity based on the index
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Entity</returns>
        public Entity this[int index]
        {
            get { return _entities[index]; }
            set { _entities[index] = value; }
        }

        /// <summary>
        /// Updates the entity manager
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entities)
            {
                entity.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the entity manager
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch</param>
        /// <param name="gameTime">The GameTime</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var entity in _entities)
            {
                entity.Draw(spriteBatch, gameTime);
            }
        }
    }
}
