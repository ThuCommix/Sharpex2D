using System.Collections.Generic;

namespace Sharpex2D.Framework.Common.Pathfinding
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class PathFinder
    {
        /// <summary>
        ///     Trys to solve a path.
        /// </summary>
        /// <param name="startField">The Startfield.</param>
        /// <param name="targetField">The Targetfield.</param>
        /// <param name="path">Out of Positions.</param>
        /// <param name="grid">The Grid.</param>
        /// <param name="algorithm">The Search-Algorithm.</param>
        /// <returns>True on success</returns>
        public static bool TrySolve(Grid grid, GridField startField, GridField targetField, IAlgorithm algorithm,
            out Stack<GridField> path)
        {
            return algorithm.TrySolve(grid, startField, targetField, out path);
        }
    }
}