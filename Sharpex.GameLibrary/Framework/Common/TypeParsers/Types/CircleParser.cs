using System;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Common.TypeParsers.Types
{
    public class CircleParser : ITypeParser
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
                var tSplit = input.Trim().Split(';');
                var x = float.Parse(tSplit[0]);
                var y = float.Parse(tSplit[1]);
                var radius = float.Parse(tSplit[2]);

                result = (T)(object)new Circle(new Vector2(x, y), radius);
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
        public Type Type { get { return typeof (Circle); } }
    }
}
