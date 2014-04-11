using System;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Common.TypeParsers.Types
{
    public class Vector2Parser : ITypeParser
    {
        /// <summary>
        /// Try to parse a object to T.
        /// </summary>
        /// <param name="input">The origin Object.</param>
        /// <param name="result">The Result.</param>
        /// <returns>True on success</returns>
        public bool TryParse<T>(string input, out T result)
        {
            try
            {
                var tSplit = input.Split(';');
                result = (T) (object)new Vector2(Convert.ToSingle(tSplit[0]), Convert.ToSingle(tSplit[1]));
                return true;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// Gets the Type of the TypeParser class.
        /// </summary>
        public Type Type {
            get { return typeof (Vector2); }
        }
    }
}
