using System.Drawing;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Common.Extensions
{
    public static class VectorExtension
    {
        /// <summary>
        /// Converts the Vector2 Array into a Point Array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Point[] ToPoints(this Vector2[] array)
        {
            var pointArray = new Point[array.Length];
            for (var i = 0; i <= array.Length - 1; i++)
            {
                pointArray[i] = new Point((int)array[i].X, (int)array[i].Y);
            }

            return pointArray;
        }
    }
}
