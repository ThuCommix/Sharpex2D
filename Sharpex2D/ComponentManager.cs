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
using System.Linq;

namespace Sharpex2D.Framework
{
    public class ComponentManager : IEnumerable<IComponent>
    {
        /// <summary>
        /// Access to the Components enumeration.
        /// </summary>
        private List<IComponent> Components { get; } = new List<IComponent>();

        /// <summary>
        /// ComponentRemoved event.
        /// </summary>
        public event EventHandler<ComponentEventArgs> ComponentRemoved;

        /// <summary>
        /// ComponentAdded event.
        /// </summary>
        public event EventHandler<ComponentEventArgs> ComponentAdded;

        /// <summary>
        /// Adds a new Component to the enumeration.
        /// </summary>
        /// <param name="component">The Component.</param>
        public void Add(IComponent component)
        {
            Components.Add(component);
            ComponentAdded?.Invoke(this, new ComponentEventArgs(component));
        }

        /// <summary>
        /// Removes a Component from the enumeration.
        /// </summary>
        /// <param name="component">The Component.</param>
        public void Remove(IComponent component)
        {
            Components.Remove(component);

            ComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
        }

        /// <summary>
        /// Returns a specific component if exists.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Component</returns>
        internal T Get<T>()
        {
            foreach (T component in Components.Where(component => component != null).OfType<T>())
            {
                return component;
            }

            throw new InvalidOperationException("Component not found (" + typeof (T).FullName + ").");
        }

        #region IEnumerable Implementation

        /// <summary>
        /// Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<IComponent> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        /// <summary>
        /// Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
