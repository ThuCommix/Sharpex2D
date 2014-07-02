// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Collections.Generic;

namespace Sharpex2D.Framework.Entities
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class EntityContainer
    {
        private readonly List<Entity> _entities;

        /// <summary>
        ///     Initializes a new EntityContainer class.
        /// </summary>
        internal EntityContainer()
        {
            _entities = new List<Entity>();
        }

        /// <summary>
        ///     Adds a new Entity to the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Add(Entity entity)
        {
            _entities.Add(entity);
        }

        /// <summary>
        ///     Removes a Entity from the Container.
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
        ///     Gets all Entities.
        /// </summary>
        /// <returns>Entity Array</returns>
        public Entity[] GetEntities()
        {
            return _entities.ToArray();
        }

        /// <summary>
        ///     Gets a special Entity.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Entity</returns>
        public T Get<T>() where T : Entity
        {
            for (int i = 0; i <= _entities.Count - 1; i++)
            {
                if (_entities[i].GetType() == typeof (T))
                {
                    return (T) _entities[i];
                }
            }

            throw new InvalidOperationException("Entity not found (" + typeof (T).FullName + ").");
        }
    }
}