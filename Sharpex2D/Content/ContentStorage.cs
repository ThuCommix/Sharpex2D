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
using System.Linq;

namespace Sharpex2D.Framework.Content
{
    public class ContentStorage<T1, T2> : Singleton<ContentStorage<T1, T2>>, IEnumerable<T2> where T2 : IContent
    {
        private readonly Dictionary<T1, T2> _storage;

        /// <summary>
        /// Initializes a new ContentStorage class.
        /// </summary>
        public ContentStorage()
        {
            _storage = new Dictionary<T1, T2>();
        }

        /// <summary>
        /// Gets the stored data.
        /// </summary>
        public T2[] Data
        {
            get { return _storage.Values.ToArray(); }
        }

        /// <summary>
        /// Gets the amount of all stored data objects.
        /// </summary>
        public int Count
        {
            get { return _storage.Values.Count; }
        }

        /// <summary>
        /// Gets the data based on the id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>TData.</returns>
        public T2 this[T1 id]
        {
            get { return _storage[id]; }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<T2> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds data to the storage.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="data">The Data.</param>
        public void Add(T1 id, T2 data)
        {
            _storage.Add(id, data);
        }

        /// <summary>
        /// Remove data based on the id.
        /// </summary>
        /// <param name="id">The Id.</param>
        public void Remove(T1 id)
        {
            if (_storage.ContainsKey(id))
            {
                _storage.Remove(id);
            }
        }

        /// <summary>
        /// Clears the storage.
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }

        /// <summary>
        /// Gets the data based on the id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>TData.</returns>
        public T2 GetById(T1 id)
        {
            return this[id];
        }
    }
}
