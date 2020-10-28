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
        /// The Terrain regions units could walk on.
        /// </summary>
        public TerrainType[] walkableRegions;
        /// <summary>
        /// Defines how much space each node is going to cover.
        /// </summary>
        public float nodeRadius = 1f;
        /// <summary>
        /// Return true to display the Grid gizmos, or false if not.
        /// </summary>
        public bool displayGridGizmos = false;

        /// <summary>
        /// A two dimensional array of nodes that represent our grid.
        /// </summary>
        Node[,] grid;
        /// <summary>
        /// A Dictionary(type, value) containing our walkable region values.
        /// </summary>
        Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();
        /// <summary>
        /// The Layers which are walkable.
        /// </summary>
        LayerMask walkableMask;
        /// <summary>
        /// The diameter of the nodes.
        /// </summary>
        float nodeDiameter;
        /// <summary>
        /// The size of the Grid in X and Y.
        /// </summary>
        int gridSizeX, gridSizeY;

        /// <summary>
        /// The maximum size of this grid.
        /// </summary>
        public int MaxSize { get { return gridSizeX * gridSizeY; } }
        #endregion

        private void Awake()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

            foreach (TerrainType region in walkableRegions)
            {
                //Here we're using a bitwise OR operator to add two binary values together so that our walkable mask contains all of our walkable terrain reions
                walkableMask.value |= region.terrainMask.value;
                walkableRegionsDictionary.Add(Mathf.RoundToInt(Mathf.Log(region.terrainMask.value, 2)), region.terrainPenelty);
            }

            CreateGrid();
        }

        /// <summary>
        /// Creates our grid.
        /// </summary>
        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];

            //The bottom left corner of our world.          //Gets left edge of world               //Bottom left corner
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                    //Collision check
                    //Going to be true if we don't collider with anything in the unwalkable mask
                    //CheckSphere checks for a collision, so if there is a collision with an unwalkable, we want to set walkable to the opposite
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                    //Calculating Node movement penelty.
                    int movementPenelty = 0;

                    //Shoot Raycasts for weights
                    if (walkable)
                    {
                        //Shooting from some point above our node.
                        Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                        RaycastHit hit;

                        //If we hit a walkable mask.
                        if (Physics.Raycast(ray, out hit, 100, walkableMask))
                        {
                            walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenelty);
                        }
                    }

                    //Populate our grid with a node.
                    grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenelty);
                }
            }
        }

        /// <summary>
        /// Gets the neighbors of the given node.
        /// </summary>
        /// <param name="node">
        /// The node whose neighbors we're getting.
        /// </param>
        /// <returns>
        /// The neighbors of the given node.
        /// </returns>
        public List<Node> GetNeighbors(Node node)
        {
            List<Node> output = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        //Skip the iteration because we're in the very middle node, when we want the neighbors
                        continue;
                    }

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    //If the place we're checking is inside the grid
                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        output.Add(grid[checkX, checkY]);
                    }
                }
            }

            return output;
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

            output = grid[x, y];

            return output;
        }

        private void OnDrawGizmos()
        {
            //TODO: Right now we'll test our pathfinding on the XZ axis, but I want to extend this later on.
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null && displayGridGizmos)
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