using System;
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
        /// <returns></returns>
        T Create(string file);
    }
}
