using Assets.Scripts.Player_Scripts.Shooting_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts
{
    /// <summary>
    /// Contains all player connected components.
    /// </summary>
    public class PlayerOverhead : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The player's movement script.
        /// </summary>
        public PlayerMovement movement { get; private set; }
        /// <summary>
        /// The player's shooting script.
        /// </summary>
        public PlayerShooting shooting { get; private set; }
        /// <summary>
        /// The class that manages placing the gun.
        /// </summary>
        public PlayerGunPlacementManager placementManager { get; private set; }
        /// <summary>
        /// The player visuals.
        /// </summary>
        public PlayerVisuals visuals { get; private set; }
        #endregion

        private void Awake()
        {
            shooting = GetComponent<PlayerShooting>();
            placementManager = GetComponent<PlayerGunPlacementManager>();
            visuals = GetComponent<PlayerVisuals>();
        }
    }
}