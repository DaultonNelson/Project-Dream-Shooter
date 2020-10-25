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
        public Node(bool _walkable, Vector3 _worldPos)
        {
            walkable = _walkable;
            worldPosition = _worldPos;
        }
    }
}
