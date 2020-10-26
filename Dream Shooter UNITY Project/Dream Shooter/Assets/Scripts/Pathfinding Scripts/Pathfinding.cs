using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public class Pathfinding : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The Transform following the path to the target.
        /// </summary>
        public Transform seeker;
        /// <summary>
        /// The Transform that represents our target.
        /// </summary>
        public Transform target;

        /// <summary>
        /// The grid this pathfinding will use.
        /// </summary>
        Grid grid;
        #endregion

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void Update()
        {
            FindPath(seeker.position, target.position);
        }

        /// <summary>
        /// Finds a path between two given points
        /// </summary>
        /// <param name="startPos">
        /// The starting position of the path.
        /// </param>
        /// <param name="targetPos">
        /// The target position of the path.
        /// </param>
        void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            List<Node> openSet = new List<Node>();
            //A HashSet is a collection of data type that prevents duplicate elements and is in no particular order
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    //We found the target
                    RetracePath(startNode, targetNode);

                    return;
                }

                foreach (Node neighbor in grid.GetNeighbors(currentNode))
                {
                    if (!neighbor.walkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retraces our path back to the beginning.
        /// </summary>
        /// <param name="startNode">
        /// The start Node.
        /// </param>
        /// <param name="endNode">
        /// The target Node.
        /// </param>
        void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();

            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();

            grid.path = path;
        }

        /// <summary>
        /// Returns the distance between the two given Nodes.
        /// </summary>
        /// <param name="nodeA">
        /// The first Node.
        /// </param>
        /// <param name="nodeB">
        /// The second Node.
        /// </param>
        /// <returns>
        /// the distance between the two given Nodes
        /// </returns>
        int GetDistance(Node nodeA, Node nodeB)
        {
            int output = 0;

            int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (distX > distY)
            {
                output = 14 * distY + (10 * (distX - distY));
            }
            else
            {
                output = 14 * distX + (10 * (distY - distX));
            }

            return output;
        }
    }
}