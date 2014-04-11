using System;

namespace Sharpex2D.Framework.Common.TypeParsers.Types
{
    public class BooleanParser : ITypeParser
    {
        /// <summary>
        /// Try to parse a object to T.
        /// </summary>
        /// <param name="input">The origin Object.</param>
        /// <param name="result">The Result.</param>
        /// <returns>True on success</returns>
        public bool TryParse<T>(string input, out T result)
        {
            var preparedString = input.Trim();
            if (preparedString == "1")
            {
                result = (T)(object)true;
                return true;
            }
            if (preparedString == "0")
            {
                result = (T)(object)false;
                return true;
            }
            if (preparedString.ToLower() == "true")
            {
                result = (T)(object)true;
                return true;
            }
            if (preparedString.ToLower() == "false")
            {
                result = (T)(object)false;
                return true;
            }
            result = (T)(object)false;
            return false;
        }
        /// <summary>
        /// Gets the Type of the TypeParser class.
        /// </summary>
        public Type Type {
            get { return typeof (bool); }
        }
    }
}
