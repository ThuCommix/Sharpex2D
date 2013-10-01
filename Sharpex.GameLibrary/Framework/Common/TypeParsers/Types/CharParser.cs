using System;

namespace SharpexGL.Framework.Common.TypeParsers.Types
{
    public class CharParser : ITypeParser
    {
        /// <summary>
        /// Try to parse a object to T.
        /// </summary>
        /// <param name="input">The origin Object.</param>
        /// <param name="result">The Result.</param>
        /// <returns>True on success</returns>
        public bool TryParse<T>(string input, out T result)
        {
            char res;
            if (char.TryParse(input, out res))
            {
                result = (T) (object) res;
                return true;
            }

            result = default(T);
            return false;
        }
        /// <summary>
        /// Gets the Type of the TypeParser class.
        /// </summary>
        public Type Type {
            get { return typeof (char); }
        } 
    }
}
