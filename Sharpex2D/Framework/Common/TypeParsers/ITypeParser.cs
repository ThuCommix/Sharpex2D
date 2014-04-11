using System;

namespace Sharpex2D.Framework.Common.TypeParsers
{
    public interface ITypeParser
    {
        /// <summary>
        /// Try to parse a object to T.
        /// </summary>
        /// <param name="input">The Input</param>
        /// <param name="result">The Result.</param>
        /// <returns>True on success</returns>
        bool TryParse<T>(string input, out T result);
        /// <summary>
        /// Gets the Type of the TypeParser class.
        /// </summary>
        Type Type { get; }
    }
}
