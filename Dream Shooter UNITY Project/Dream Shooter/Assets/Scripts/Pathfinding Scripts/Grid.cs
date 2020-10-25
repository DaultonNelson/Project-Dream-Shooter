using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public class Grid : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The layers that are deemed unwalkable.
        /// </summary>
        public LayerMask unwalkableMask;
        /// <summary>
        /// Defines the area in world coordinates that the grid is going to cover.
        /// </summary>
        public Vector2 gridWorldSize = Vector2.one;
        /// <summary>
        /// Defines how much space each node is going to cover.
        /// </summary>
        public float nodeRadius = 1f;

        /// <summary>
        /// A two dimensional array of nodes that represent our grid.
        /// </summary>
        Node[,] grid;
        /// <summary>
        /// The diameter of the nodes.
        /// </summary>
        float nodeDiameter;
        /// <summary>
        /// The size of the Grid in X and Y.
        /// </summary>
        int gridSizeX, gridSizeY;
        #endregion

        private void Start()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
            CreateGrid();
        }

        /// <summary>
        /// Creates our grid.
        /// </summary>
        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];

            //The bottom left corner of our world.          //Gets left edge of world               //Bottom left corner
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                    //Collision check
                    //Going to be true if we don't collider with anything in the unwalkable mask
                    //CheckSphere checks for a collision, so if there is a collision with an unwalkable, we want to set walkable to the opposite
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                    //Populate our grid with a node.
                    grid[x,y] = new Node(walkable, worldPoint);
                }
            }
        }

        /// <summary>
        /// Returns a Node from a given world point.
        /// </summary>
        /// <param name="worldPosition">
        /// The world point being parsed.
        /// </param>
        /// <returns>
        /// The node based off the given world point.
        /// </returns>
        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            Node output;

            float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

            //Clamps our percentages so that they stay between 0 and 1
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            output = grid[x,y];

            return output;
        }

        private void OnDrawGizmos()
        {
            //TODO: Right now we'll test our pathfinding on the XZ axis, but I want to extend this later on.
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    //If the node is walkable it's white, if not it's red
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }

    }
}