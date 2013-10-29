using System.Collections.Generic;

namespace SharpexGL.Framework.Common.Pathfinding.AStar
{
    public class AStarAlgorithm : IAlgorithm
    {
        /// <summary>
        /// Trys to solve a path.
        /// </summary>
        /// <param name="startField">The Startfield.</param>
        /// <param name="targetField">The Targetfield.</param>
        /// <param name="path">Out of Positions.</param>
        /// <param name="grid">The Grid.</param>
        /// <returns>True on success</returns>
        public bool TrySolve(Grid grid, GridField startField, GridField targetField, out Stack<GridField> path)
        {
            path = null;
            startField.Predecessor = null;
            startField.G = 0;

            if (!startField.IsWalkable)
                return false;

            var openList = new List<GridField> {startField};
            var closedList = new List<GridField>();

            do
            {
                var currentNode = openList[0];
                openList.RemoveAt(0);

                if (currentNode == targetField)
                {
                    path = new Stack<GridField>();
                    var node = currentNode;
                    while (node != null)
                    {
                        path.Push(node);
                        node = node.Predecessor;
                    }

                    return true;
                }

                closedList.Add(currentNode);

                foreach (var t in currentNode.Neighbors)
                {
                    var neighbor = grid.GetGridField(t.X, t.Y);

                    if (!neighbor.IsWalkable || closedList.Contains(neighbor))
                        continue;

                    float g = currentNode.G + currentNode.DistanceToNeighbor(neighbor);

                    bool isInOpenList = openList.Contains(neighbor);
                    if (isInOpenList && g >= neighbor.G)
                        continue;

                    neighbor.Predecessor = currentNode;
                    neighbor.G = g;
                    neighbor.F = g + grid.GetDistance(neighbor, targetField);
                    if (!isInOpenList) openList.Add(neighbor);
                }

                openList.Sort();
            } while (openList.Count > 0);

            return false;
        }
    }
}
