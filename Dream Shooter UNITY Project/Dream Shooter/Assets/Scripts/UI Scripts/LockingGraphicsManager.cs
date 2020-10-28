﻿using Assets.Scripts.Player_Scripts;
using Assets.Scripts.Player_Scripts.Shooting_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI_Scripts
{
    public class LockingGraphicsManager : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The movement Component of the player.
        /// </summary>
        public PlayerMovement playerMovement;
        /// <summary>
        /// The shooting Component of the player.
        /// </summary>
        public PlayerShooting playerShooting;
        /// <summary>
        /// The Locking Icon for movement.
        /// </summary>
        public UILockingIcon movementIcon;
        /// <summary>
        /// The Locking Icon for shooting.
        /// </summary>
        public UILockingIcon shootingIcon;
        /// <summary>
        /// The color the BG should be when the graphic is locked.
        /// </summary>
        public Color BGLockedColor = Color.white;
        #endregion

        void Update()
        {
            CheckForLocking(movementIcon, playerMovement.movementLocked);
            CheckForLocking(shootingIcon, playerShooting.gunLocked);
        }

        private void CheckForLocking(UILockingIcon icon, bool locked)
        {
            if (locked)
            {
                icon.backgroundSprite.color = BGLockedColor;
                icon.lockSprite.color = Color.white;
                icon.inputKeySprite.color = Color.white;
            }
            else
            {
                icon.backgroundSprite.color = icon.initBGColor;
                icon.lockSprite.color = icon.initLockColor;
                icon.inputKeySprite.color = icon.initKeyColor;
            }
        }
    } 
}