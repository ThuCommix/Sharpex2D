using System;
using System.IO;

namespace SharpexGL.Framework.Content.Factory
{
    public interface IFactory<out T>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        Type FactoryType { get; }
        /// <summary>
        /// Creates a new T Instance.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>T</returns>
        T Create(string file);
        /// <summary>
        /// Creates a new T Instance.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>T</returns>
        T Create(Stream stream);
    }
}
