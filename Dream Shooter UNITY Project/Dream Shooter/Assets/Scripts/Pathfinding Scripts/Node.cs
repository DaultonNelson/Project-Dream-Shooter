using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public class Node
    {
        #region Variables
        /// <summary>
        /// Returns true if not is walkable, or false if not.
        /// </summary>
        public bool walkable { get; set; }
        /// <summary>
        /// The node's position in the game world.
        /// </summary>
        public Vector3 worldPosition { get; set; }
        /// <summary>
        /// The X position of this Node in it's grid.
        /// </summary>
        public int gridX { get; set; }
        /// <summary>
        /// The Y position of this Node in it's grid.
        /// </summary>
        public int gridY { get; set; }
        /// <summary>
        /// The distance from the starting Node.
        /// </summary>
        public int gCost { get; set; }
        /// <summary>
        /// The distance from the target Node.
        /// </summary>
        public int hCost { get; set; }
        /// <summary>
        /// Returns the F Cost of this node (gCost + hCost).
        /// </summary>
        public int fCost { get { return gCost + hCost; } }
        /// <summary>
        /// The parent of this node.
        /// </summary>
        public Node parent { get; set; }
        #endregion

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="_walkable">
        /// Determines whether the node is walkable or not.
        /// </param>
        /// <param name="_worldPos">
        /// The node's world position.
        /// </param>
        /// <param name="_gridX">
        /// The node's X position in it's grid.
        /// </param>
        /// <param name="_gridY">
        /// The node's Y position in it's grid.
        /// </param>
        public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
        {
            walkable = _walkable;
            worldPosition = _worldPos;
            gridX = _gridX;
            gridY = _gridY;
        }
    }
}
