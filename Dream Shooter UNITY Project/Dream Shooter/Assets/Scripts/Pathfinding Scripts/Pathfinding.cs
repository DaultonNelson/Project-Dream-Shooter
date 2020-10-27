using System;
using System.Collections; //Allows me to use IEnumerable in the way I expect
using System.Collections.Generic;
using System.Diagnostics;
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
        /// The grid this pathfinding will use.
        /// </summary>
        Grid grid;
        /// <summary>
        /// The Path Request Manager class reference.
        /// </summary>
        PathRequestManager requestManager;
        #endregion

        private void Awake()
        {
            grid = GetComponent<Grid>();
            requestManager = GetComponent<PathRequestManager>();
        }

        /// <summary>
        /// Finds a path between two given points.
        /// </summary>
        /// <param name="startPos">
        /// The starting position of the path.
        /// </param>
        /// <param name="targetPos">
        /// The target position of the path.
        /// </param>
        IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
        {
            //Times the execution of our code, good to use to compare performance cost of code.
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //The waypoints of our path.
            Vector3[] waypoints = new Vector3[0];
            //Will only be true if we actually find a path.
            bool pathSuccess = false;

            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            if (startNode.walkable && targetNode.walkable)
            {
                Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
                //A HashSet is a collection of data type that prevents duplicate elements and is in no particular order
                HashSet<Node> closedSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        sw.Stop();

                        UnityEngine.Debug.Log("Path found: " + sw.ElapsedMilliseconds + " ms");

                        pathSuccess = true;

                        break;
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
            yield return null;

            //We found the target
            RetracePath(startNode, targetNode);

            if (pathSuccess)
            {
                //We found the target and are making waypoints to it
                waypoints = RetracePath(startNode, targetNode);
            }
            requestManager.FinishedProcessingPath(waypoints, pathSuccess);
        }

        /// <summary>
        /// Starts finding a path.
        /// </summary>
        /// <param name="startPos">
        /// The path's start.
        /// </param>
        /// <param name="targetPos">
        /// The path's end.
        /// </param>
        public void StartFindPath(Vector3 startPos, Vector3 targetPos)
        {
            StartCoroutine(FindPath(startPos, targetPos));
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
        Vector3[] RetracePath(Node startNode, Node endNode)
        {
            Vector3[] output = new Vector3[0];

            List<Node> path = new List<Node>();

            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            output = SimplifyPath(path);

            //output.Reverse(); //Doesn't work, and doesn't return a compile time error
            Array.Reverse(output);

            return output;
        }

        /// <summary>
        /// Simplifies the given path.
        /// </summary>
        /// <param name="path">
        /// The path that needs simplification.
        /// </param>
        /// <returns>
        /// A simplified path.
        /// </returns>
        Vector3[] SimplifyPath(List<Node> path)
        {
            Vector3[] output = new Vector3[0];

            List<Vector3> waypoints = new List<Vector3>();
            //The direction of the last two nodes.
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                
                //If the path has changed direction
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i].worldPosition);
                }
                
                directionOld = directionNew;
            }

            output = waypoints.ToArray();

            return output;
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