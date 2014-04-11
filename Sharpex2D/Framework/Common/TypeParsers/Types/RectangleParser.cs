using System;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Common.TypeParsers.Types
{
    public class RectangleParser : ITypeParser
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
                var width = float.Parse(tSplit[2]);
                var height = float.Parse(tSplit[3]);

                result = (T)(object)new Rectangle(x, y, width, height);
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
        public Type Type { get { return typeof (Rectangle); } }
    }
}
