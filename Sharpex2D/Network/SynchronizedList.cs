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

using System.Collections;
using System.Collections.Generic;

namespace Sharpex2D.Framework.Network
{
    internal class SynchronizedList<T> : IList<T>
    {
        private readonly List<T> _internalList;
        private readonly object _locker;

        /// <summary>
        /// Initializes a new SynchronizedList class.
        /// </summary>
        public SynchronizedList()
        {
            _locker = new object();
            _internalList = new List<T>();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Clone().GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>Enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The Item.</param>
        public void Add(T item)
        {
            lock (_locker)
            {
                _internalList.Add(item);
            }
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            lock (_locker)
            {
                _internalList.Clear();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the list contains the item.
        /// </summary>
        /// <param name="item">The Item.</param>
        /// <returns>True if the item is in the list.</returns>
        public bool Contains(T item)
        {
            lock (_locker)
            {
                return _internalList.Contains(item);
            }
        }

        /// <summary>
        /// Copies all items from the list at the specified array index.
        /// </summary>
        /// <param name="array">The Array.</param>
        /// <param name="arrayIndex">The ArrayIndex.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_locker)
            {
                _internalList.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        /// <param name="item">The Item.</param>
        /// <returns>True on success.</returns>
        public bool Remove(T item)
        {
            lock (_locker)
            {
                return _internalList.Remove(item);
            }
        }

        /// <summary>
        /// Gets the amount of items.
        /// </summary>
        public int Count => _internalList.Count;

        /// <summary>
        /// A value indicating whether the list is read only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the index of the specified item.
        /// </summary>
        /// <param name="item">The Item.</param>
        /// <returns>Index.</returns>
        public int IndexOf(T item)
        {
            lock (_locker)
            {
                return _internalList.IndexOf(item);
            }
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="item">The Item.</param>
        public void Insert(int index, T item)
        {
            lock (_locker)
            {
                _internalList.Insert(index, item);
            }
        }

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index">The Index.</param>
        public void RemoveAt(int index)
        {
            lock (_locker)
            {
                _internalList.RemoveAt(index);
            }
        }

        /// <summary>
        /// Gets or sets an item at the specified index.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>TItem.</returns>
        public T this[int index]
        {
            get
            {
                lock (_locker)
                {
                    return _internalList[index];
                }
            }
            set
            {
                lock (_locker)
                {
                    _internalList[index] = value;
                }
            }
        }

        /// <summary>
        /// Clones the list.
        /// </summary>
        /// <returns>ListOfT.</returns>
        private List<T> Clone()
        {
            var iteratorList = new List<T>();

            lock (_locker)
            {
                iteratorList.AddRange(_internalList);
            }

            return iteratorList;
        }
    }
}
