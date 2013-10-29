using System;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Common.Pathfinding
{
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
        /// Gets the distance to a neighbor.
        /// </summary>
        /// <param name="neighbor">The Neigbor.</param>
        /// <returns>Int32</returns>
        public int DistanceToNeighbor(GridField neighbor)
        {
            var dX = (int)Position.X - (int)neighbor.Position.X;
            var dY = (int)Position.Y - (int)neighbor.Position.Y;
            return   (int)MathHelper.Sqrt(dX * dX + dY * dY);
        }
        /// <summary>
        /// Compares the fieldvalue to other.
        /// </summary>
        /// <param name="other">The other GridField.</param>
        /// <returns>Int32</returns>
        public int CompareTo(GridField other)
        {
            return F.CompareTo(other.F);
        }
    }
}
