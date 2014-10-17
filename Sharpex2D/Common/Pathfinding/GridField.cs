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

using System;
using Sharpex2D.Math;

namespace Sharpex2D.Common.Pathfinding
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GridField : IComparable<GridField>
    {
        /// <summary>
        /// Initializes a new GridField class.
        /// </summary>
        public GridField()
        {
            IsWalkable = true;
        }

        internal float G { get; set; }

        internal float F { get; set; }

        internal GridField Predecessor { set; get; }

        /// <summary>
        /// Gets the Neigbors of the GridField.
        /// </summary>
        public Neighbor[] Neighbors { internal set; get; }

        /// <summary>
        /// Gets the Position.
        /// </summary>
        public Vector2 Position { internal set; get; }

        /// <summary>
        /// A value indicating whether the field is walkable.
        /// </summary>
        public bool IsWalkable { set; get; }

        /// <summary>
        /// Compares the fieldvalue to other.
        /// </summary>
        /// <param name="other">The other GridField.</param>
        /// <returns>Int32</returns>
        public int CompareTo(GridField other)
        {
            return F.CompareTo(other.F);
        }

        /// <summary>
        /// Gets the distance to a neighbor.
        /// </summary>
        /// <param name="neighbor">The Neigbor.</param>
        /// <returns>Int32</returns>
        public int DistanceToNeighbor(GridField neighbor)
        {
            int dX = (int) Position.X - (int) neighbor.Position.X;
            int dY = (int) Position.Y - (int) neighbor.Position.Y;
            return (int) MathHelper.Sqrt(dX*dX + dY*dY);
        }
    }
}