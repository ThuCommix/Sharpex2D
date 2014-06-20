namespace Sharpex2D.Framework.Common.Pathfinding
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Neighbor
    {
        /// <summary>
        ///     Initializes a new Neighbor class.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        internal Neighbor(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Gets the X-Coord.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        ///     Gets the Y-Coord.
        /// </summary>
        public int Y { get; private set; }
    }
}