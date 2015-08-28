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
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ComponentManager : IConstructable, IEnumerable<IComponent>
    {
        private bool _alreadyCalledConstruct;

        /// <summary>
        /// Access to the Components enumeration.
        /// </summary>
        private List<IComponent> Components { get; } = new List<IComponent>();

        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("6A3D114D-6DF4-429E-82ED-F7CD0AE29CF8"); }
        }

        #endregion

        /// <summary>
        /// Initializes all Components.
        /// </summary>
        public void Construct()
        {
            for (int i = 0; i <= Components.Count - 1; i++)
            {
                var com = Components[i] as IConstructable;
                if (com != null)
                {
                    com.Construct();
                }
            }
            _alreadyCalledConstruct = true;
        }

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
            if (_alreadyCalledConstruct)
            {
                //Single Construct.
                var com = component as IConstructable;
                if (com != null)
                {
                    com.Construct();
                }
            }

            if (ComponentAdded != null)
            {
                ComponentAdded(this, new ComponentEventArgs(component));
            }
        }

        /// <summary>
        /// Removes a Component from the enumeration.
        /// </summary>
        /// <param name="component">The Component.</param>
        public void Remove(IComponent component)
        {
            Components.Remove(component);

            if (ComponentRemoved != null)
            {
                ComponentRemoved(this, new ComponentEventArgs(component));
            }
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

        /// <summary>
        /// Gets the Component by Guid.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>IComponent</returns>
        public IComponent GetByGuid(Guid guid)
        {
            foreach (IComponent component in Components)
            {
                if (component == null) continue;
                if (component.Guid == guid)
                {
                    return component;
                }
            }

            throw new InvalidOperationException("Component with guid " + guid + " not found.");
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