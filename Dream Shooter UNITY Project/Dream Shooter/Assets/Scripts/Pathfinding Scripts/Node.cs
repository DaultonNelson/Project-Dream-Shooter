using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public class Node : IHeapItem<Node>
    {
        #region Variables
        /// <summary>
        /// Returns true if Node is walkable, or false if not.
        /// </summary>
        public bool walkable { get; set; }
        /// <summary>
        /// The Node's position in the game world.
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
        /// Returns the F Cost of this Node (gCost + hCost).
        /// </summary>
        public int fCost { get { return gCost + hCost; } }
        /// <summary>
        /// The parent of this Node.
        /// </summary>
        public Node parent { get; set; }

        /// <summary>
        /// The index of this Node within a Heap.
        /// </summary>
        int heapIndex;

        /// <summary>
        /// Implemented from IHeapItem, the index of this Node within a Heap.
        /// </summary>
        public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }
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

        /// <summary>
        /// Implemented from IComparable, Compares one object (Node) to another for it's priority.
        /// </summary>
        /// <param name="other">
        /// The other Node this Node is being compared to.
        /// </param>
        /// <returns>
        /// 1 if other Node's priority is higher than this Node's, if same returns 0, if lower returns -1.
        /// </returns>
        public int CompareTo(Node other)
        {
            int output = 5;

            //Is the other's fCost higher that this Node's fCost
            int compare = fCost.CompareTo(other.fCost);

            //Both fCosts are the same value
            if (compare == 0)
            {
                //Is the other's hCost higher that this Node's hCost
                compare = hCost.CompareTo(other.hCost);
            }

            //If the comparison was higher, that means the other node needs to have a lower priority, so we want the inverse number
            output = -compare;

            return output;
        }
    }
}
