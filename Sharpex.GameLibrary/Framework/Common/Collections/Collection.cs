using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.Common.Collections
{
    public class Collection<T>
    {
        /// <summary>
        /// Initializes a new Collection class.
        /// </summary>
        public Collection()
        {
            _elements = new List<T>();
        }

        private readonly List<T> _elements;

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
            for (var i = 0; i < _elements.Count -1; i++)
            {
                if (_elements[i].GetType() == typeof (TE))
                {
                    return (TE)(object) _elements[i];
                }
            }

            throw new InvalidOperationException("Element not found (" + typeof(T).FullName + ").");
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
    }
}
