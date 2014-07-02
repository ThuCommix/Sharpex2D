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

namespace Sharpex2D.Framework.Common.Pathfinding.AStar
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class AStarAlgorithm : IAlgorithm
    {
        /// <summary>
        ///     Trys to solve a path.
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
                GridField currentNode = openList[0];
                openList.RemoveAt(0);

                if (currentNode == targetField)
                {
                    path = new Stack<GridField>();
                    GridField node = currentNode;
                    while (node != null)
                    {
                        path.Push(node);
                        node = node.Predecessor;
                    }

                    return true;
                }

                closedList.Add(currentNode);

                foreach (Neighbor t in currentNode.Neighbors)
                {
                    GridField neighbor = grid.GetGridField(t.X, t.Y);

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