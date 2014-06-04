using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ContentStorage : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("8729597F-B800-47BA-83AB-A259460ABB60"); }
        }

        #endregion

        private readonly Dictionary<string, IContent> _storage;

        /// <summary>
        ///     Initializes a new ContentStorage class.
        /// </summary>
        public ContentStorage()
        {
            _storage = new Dictionary<string, IContent>();
        }

        /// <summary>
        ///     Gets the amount of stored content.
        /// </summary>
        public int ContentCount
        {
            get { return _storage.Count; }
        }

        /// <summary>
        ///     Adds a new IContent with a specific key to the storage.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <param name="content">The Content.</param>
        public void Add(string key, IContent content)
        {
            if (!_storage.ContainsKey(key))
            {
                _storage.Add(key, content);
            }
            else
            {
                throw new ArgumentException("The key already exists.");
            }
        }

        /// <summary>
        ///     Removes the Content with the specific key.
        /// </summary>
        /// <param name="key">The Key.</param>
        public void Remove(string key)
        {
            if (!_storage.ContainsKey(key))
            {
                throw new ArgumentException("The key was not found.");
            }

            _storage.Remove(key);
        }

        /// <summary>
        ///     Clears the storage.
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }

        /// <summary>
        ///     Gets the Content and removes it from the storage.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>IContent</returns>
        public IContent GetContentAndRemove(string key)
        {
            if (!_storage.ContainsKey(key))
            {
                throw new ArgumentException("The key was not found.");
            }

            IContent content = _storage[key];
            _storage.Remove(key);
            return content;
        }

        /// <summary>
        ///     Gets the Content.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>IContent</returns>
        public IContent GetContent(string key)
        {
            if (!_storage.ContainsKey(key))
            {
                throw new ArgumentException("The key was not found.");
            }

            return _storage[key];
        }
    }
}