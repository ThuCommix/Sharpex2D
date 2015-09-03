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
    public class GameComponentCollection : IList<GameComponent>
    {
        private readonly DrawOrderComparer _drawOrderComparer;
        private readonly List<GameComponent> _gameComponents;
        private readonly UpdateOrderComparer _updateOrderComparer;

        /// <summary>
        /// Initializes a new GameComponentCollection class.
        /// </summary>
        public GameComponentCollection()
        {
            _gameComponents = new List<GameComponent>();
            _drawOrderComparer = new DrawOrderComparer();
            _updateOrderComparer = new UpdateOrderComparer();
            SyncRoot = new object();
        }

        /// <summary>
        /// A value indicating whether the list has a fixed size.
        /// </summary>
        public bool IsFixedSize => false;

        /// <summary>
        /// Gets the sync root.
        /// </summary>
        public object SyncRoot { get; private set; }

        /// <summary>
        /// A value indicating whether the list is synchronized.
        /// </summary>
        public bool IsSynchronized => false;

        /// <summary>
        /// Adds a new component.
        /// </summary>
        /// <param name="component">The GameComponent.</param>
        public void Add(GameComponent component)
        {
            _gameComponents.Add(component);
            ComponentAdded?.Invoke(this, new GameComponentEventArgs(component));
        }

        /// <summary>
        /// Copies the elements on the specified array at the specified index.
        /// </summary>
        /// <param name="array">The Array.</param>
        /// <param name="arrayIndex">The Index.</param>
        public void CopyTo(GameComponent[] array, int arrayIndex)
        {
            _gameComponents.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes a component.
        /// </summary>
        /// <param name="component">The GameComponent.</param>
        public bool Remove(GameComponent component)
        {
            if (_gameComponents.Contains(component))
            {
                _gameComponents.Remove(component);
                ComponentRemoved?.Invoke(this, new GameComponentEventArgs(component));

                _gameComponents.Sort(new UpdateOrderComparer());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the list contains the specified component.
        /// </summary>
        /// <param name="value">The GameComponent.</param>
        /// <returns>True if the list contains the component.</returns>
        public bool Contains(GameComponent value)
        {
            return _gameComponents.Contains(value);
        }

        /// <summary>
        /// Clears the component list.
        /// </summary>
        public void Clear()
        {
            _gameComponents.Clear();
        }

        /// <summary>
        /// Gets the index for the specified component.
        /// </summary>
        /// <param name="value">The GameComponent.</param>
        /// <returns></returns>
        public int IndexOf(GameComponent value)
        {
            return _gameComponents.IndexOf(value);
        }

        /// <summary>
        /// Inserts a component at the specified index.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="value">The GameComponent.</param>
        public void Insert(int index, GameComponent value)
        {
            _gameComponents.Insert(index, value);
        }

        /// <summary>
        /// Removes the component at the specified index.
        /// </summary>
        /// <param name="index">The Index.</param>
        public void RemoveAt(int index)
        {
            _gameComponents.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets a component.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>GameComponent.</returns>
        public GameComponent this[int index]
        {
            get { return _gameComponents[index]; }
            set { _gameComponents[index] = value; }
        }

        /// <summary>
        /// A value indicating whether the list is read only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count => _gameComponents.Count;

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>Enumaerator.</returns>
        public IEnumerator<GameComponent> GetEnumerator()
        {
            return _gameComponents.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>Enumaerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Raises when a component was added.
        /// </summary>
        public event EventHandler<GameComponentEventArgs> ComponentAdded;

        /// <summary>
        /// Raises when a component was removed.
        /// </summary>
        public event EventHandler<GameComponentEventArgs> ComponentRemoved;

        /// <summary>
        /// Gets the updateables.
        /// </summary>
        /// <returns>Enumerable.</returns>
        internal IEnumerable<GameComponent> GetUpdateables()
        {
            _gameComponents.Sort(_updateOrderComparer);
            return _gameComponents;
        }

        /// <summary>
        /// Gets the drawables.
        /// </summary>
        /// <returns>Enumerable.</returns>
        internal IEnumerable<DrawableGameComponent> GetDrawables()
        {
            List<DrawableGameComponent> list = _gameComponents.OfType<DrawableGameComponent>().ToList();
            list.Sort(_drawOrderComparer);
            return list;
        }

        public class DrawOrderComparer : IComparer<DrawableGameComponent>
        {
            /// <summary>
            /// Compares two drawable game components.
            /// </summary>
            /// <param name="x">X.</param>
            /// <param name="y">Y.</param>
            /// <returns>Int32.</returns>
            public int Compare(DrawableGameComponent x, DrawableGameComponent y)
            {
                return x.DrawOrder.CompareTo(y.DrawOrder);
            }
        }

        public class UpdateOrderComparer : IComparer<GameComponent>
        {
            /// <summary>
            /// Compares two game components.
            /// </summary>
            /// <param name="x">X.</param>
            /// <param name="y">Y.</param>
            /// <returns>Int32.</returns>
            public int Compare(GameComponent x, GameComponent y)
            {
                return x.UpdateOrder.CompareTo(y.UpdateOrder);
            }
        }
    }
}
