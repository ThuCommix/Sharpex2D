// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections.Generic;
using Sharpex2D.Math;

namespace Sharpex2D.Common.Pathfinding
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Grid
    {
        private readonly GridField[,] _fields;

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
            for (int x = 0; x <= width - 1; x++)
            {
                for (int y = 0; y <= height - 1; y++)
                {
                    //detect neighbors
                    var gridField = new GridField {Neighbors = GetNeighbors(x, y), Position = new Vector2(x, y)};
                    _fields[x, y] = gridField;
                }
            }
        }

        public int GridWidth { private set; get; }
        public int GridHeight { private set; get; }

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
            int dX = (int) first.Position.X - (int) second.Position.X;
            int dY = (int) first.Position.Y - (int) second.Position.Y;
            return (int) MathHelper.Sqrt(dX*dX + dY*dY);
        }
    }
}