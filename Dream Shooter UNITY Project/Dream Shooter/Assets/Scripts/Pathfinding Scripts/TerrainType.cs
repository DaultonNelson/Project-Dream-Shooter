using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    [System.Serializable]
    public class TerrainType
    {
        #region Variables
        //TODO: Restict this to only one layer, otherwise crash game
        /// <summary>
        /// The Layer this terrain belongs to.
        /// </summary>
        public LayerMask terrainMask;
        /// <summary>
        /// The penelty for traversing this terrain.
        /// </summary>
        public int terrainPenelty;
        #endregion
    }
}