using System;
using System.Collections.Generic;

namespace Sharpex2D.Framework.Content.Storage
{
    public static class ContentStorage
    {
        /// <summary>
        /// Initializes a new ContentStorage class.
        /// </summary>
        static ContentStorage()
        {
            Storage = new Dictionary<string, IContent>();
        }

        private static readonly Dictionary<string, IContent> Storage;

        /// <summary>
        /// Gets the amount of stored content.
        /// </summary>
        public static int ContentCount { get { return Storage.Count; } }

        /// <summary>
        /// Adds a new IContent with a specific key to the storage.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <param name="content">The Content.</param>
        public static void Add(string key, IContent content)
        {
            if (!Storage.ContainsKey(key))
            {
                Storage.Add(key, content);
            }
            else
            {
                throw new ArgumentException("The key already exists.");
            }
        }

        /// <summary>
        /// Removes the Content with the specific key.
        /// </summary>
        /// <param name="key">The Key.</param>
        public static void Remove(string key)
        {
            if (!Storage.ContainsKey(key))
            {
                throw new ArgumentException("The key was not found.");
            }

            Storage.Remove(key);
        }

        /// <summary>
        /// Clears the storage.
        /// </summary>
        public static void Clear()
        {
            Storage.Clear();
        }

        /// <summary>
        /// Gets the Content and removes it from the storage.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>IContent</returns>
        public static IContent GetContentAndRemove(string key)
        {
            if (!Storage.ContainsKey(key))
            {
                throw new ArgumentException("The key was not found.");
            }

            var content = Storage[key];
            Storage.Remove(key);
            return content;
        }

        /// <summary>
        /// Gets the Content.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>IContent</returns>
        public static IContent GetContent(string key)
        {
            if (!Storage.ContainsKey(key))
            {
                throw new ArgumentException("The key was not found.");
            }

            return Storage[key];
        }
    }
}
