using System.Drawing;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Common.Extensions
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class VectorExtension
    {
        /// <summary>
        ///     Converts the Vector2 Array into a Point Array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Point[] ToPoints(this Vector2[] array)
        {
            var pointArray = new Point[array.Length];
            for (int i = 0; i <= array.Length - 1; i++)
            {
                pointArray[i] = new Point((int) array[i].X, (int) array[i].Y);
            }

            return pointArray;
        }
    }
}