using System.Collections.Generic;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Common.Pathfinding
{
    public class Grid
    {
        /// <summary>
        /// Initializes a new Grid class.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public Grid(int width, int height)
        {
            _fields = new GridField[width, height];
            GridWidth = width;
            GridHeight = height;
            for (var x = 0; x <= width - 1; x++)
            {
                for (var y = 0; y <= height - 1; y++)
                {
                    //detect neighbors
                    var gridField = new GridField {Neighbors = GetNeighbors(x, y), Position = new Vector2(x, y)};
                    _fields[x, y] = gridField;
                }
            }
        }

        public int GridWidth { private set; get; }
        public int GridHeight { private set; get; }
        private readonly GridField[,] _fields;

        /// <summary>
        /// Returns all Neighbors of a field.
        /// </summary>
        /// <param name="x">The X-Coord of the field.</param>
        /// <param name="y">The Y-Coord of the field.</param>
        /// <returns>Neighbor Array</returns>
        private Neighbor[] GetNeighbors(int x, int y)
        {
            var neighbors = new List<Neighbor>();

            //left neighbor

            if (x - 1 >= 0)
            {
                neighbors.Add(new Neighbor(x - 1, y));
            }

            //right neighbor

            if (x + 1 <= GridWidth - 1)
            {
                neighbors.Add(new Neighbor(x + 1, y));
            }

            //upper neighbor

            if (y - 1 >= 0)
            {
                neighbors.Add(new Neighbor(x, y - 1));
            }

            //bottom neighbor

            if (y + 1 <= GridHeight - 1)
            {
                neighbors.Add(new Neighbor(x, y + 1));
            }

            return neighbors.ToArray();
        }

        /// <summary>
        /// Returns a GridField on the specific position.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <returns>GridField</returns>
        public GridField GetGridField(int x, int y)
        {
            return _fields[x, y];
        }

        /// <summary>
        /// Gets the distance between two GridFields.
        /// </summary>
        /// <param name="first">The first GridField.</param>
        /// <param name="second">The second GridField.</param>
        /// <returns>Int32</returns>
        public int GetDistance(GridField first, GridField second)
        {
            var dX = (int)first.Position.X - (int)second.Position.X;
            var dY = (int)first.Position.Y - (int)second.Position.Y;
            return (int)MathHelper.Sqrt(dX * dX + dY * dY);
        }
    }
}
