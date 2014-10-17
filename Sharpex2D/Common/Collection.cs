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

namespace Sharpex2D.Common
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Collection<T>
    {
        private readonly List<T> _elements;

        /// <summary>
        /// Initializes a new Collection class.
        /// </summary>
        public Collection()
        {
            _elements = new List<T>();
        }

        /// <summary>
        /// Gets the Element by Index.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>T</returns>
        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < _elements.Count)
                {
                    return _elements[index];
                }

                throw new ArgumentException("index");
            }
            set
            {
                if (index >= 0 && index < _elements.Count)
                {
                    _elements[index] = value;
                }
                else
                {
                    throw new ArgumentException("index");
                }
            }
        }

        /// <summary>
        /// Adds a new Element to the collection.
        /// </summary>
        /// <param name="element">The Element.</param>
        public void Add(T element)
        {
            _elements.Add(element);
        }

        /// <summary>
        /// Removes an Element from the collection.
        /// </summary>
        /// <param name="element">The Element.</param>
        public void Remove(T element)
        {
            if (_elements.Contains(element))
            {
                _elements.Remove(element);
            }
        }

        /// <summary>
        /// Indicates whether the collection contains an element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(T element)
        {
            return _elements.Contains(element);
        }

        /// <summary>
        /// Gets all elements.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return _elements.ToArray();
        }

        /// <summary>
        /// Gets an specified Element of the collection.
        /// </summary>
        /// <typeparam name="TE">The Type.</typeparam>
        /// <returns>Element</returns>
        public TE Get<TE>()
        {
            for (int i = 0; i < _elements.Count - 1; i++)
            {
                if (_elements[i].GetType() == typeof (TE))
                {
                    return (TE) (object) _elements[i];
                }
            }

            throw new InvalidOperationException("Element not found (" + typeof (T).FullName + ").");
        }
    }
}