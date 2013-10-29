using System.Collections.Generic;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Common.Pathfinding
{
    public interface IAlgorithm
    {
        /// <summary>
        /// Trys to solve a path.
        /// </summary>
        /// <param name="startField">The Startfield.</param>
        /// <param name="targetField">The Targetfield.</param>
        /// <param name="path">Out of Positions.</param>
        /// <param name="grid">The Grid.</param>
        /// <returns>True on success</returns>
        bool TrySolve(Grid grid, GridField startField, GridField targetField, out Stack<GridField> path);
    }
}
