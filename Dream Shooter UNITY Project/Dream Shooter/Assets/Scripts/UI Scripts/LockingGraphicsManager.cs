using Assets.Scripts.Shooting_Scripts;
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
        /// The Background for the Move Lock graphic.
        /// </summary>
        [Space(10)]
        public Image moveLock_BG;
        /// <summary>
        /// The Lock icon for the Move Lock graphic.
        /// </summary>
        public Image moveLock_Lock;
        /// <summary>
        /// The Key icon for the Move Lock graphic.
        /// </summary>
        public Image moveLock_Key;

        /// <summary>
        /// The Background for the Gun Lock graphic.
        /// </summary>
        [Space(10)]
        public Image gunLock_BG;
        /// <summary>
        /// The Lock icon for the Gun Lock graphic.
        /// </summary>
        public Image gunLock_Lock;
        /// <summary>
        /// The Key icon for the Gun Lock graphic.
        /// </summary>
        public Image gunLock_Key;
        /// <summary>
        /// The color the BG should be when the graphic is locked.
        /// </summary>
        public Color BGLockedColor = Color.white;

        /// <summary>
        /// The initial color of the BG graphic.
        /// </summary>
        private Color initBGColor;
        /// <summary>
        /// The initial color of the lock.
        /// </summary>
        private Color initLockColor;
        /// <summary>
        /// The initial color of the key.
        /// </summary>
        private Color initKeyColor;
        #endregion

        private void Start()
        {
            initBGColor = moveLock_BG.color;
            initLockColor = moveLock_Lock.color;
            initKeyColor = moveLock_Key.color;
        }

        void Update()
        {
            if (playerMovement.movementLocked)
            {
                moveLock_BG.color = BGLockedColor;
                moveLock_Lock.color = Color.white;
                moveLock_Key.color = Color.white;
            }
            else
            {
                moveLock_BG.color = initBGColor;
                moveLock_Lock.color = initLockColor;
                moveLock_Key.color = initKeyColor;
            }

            if (playerShooting.gunLocked)
            {
                gunLock_BG.color = BGLockedColor;
                gunLock_Lock.color = Color.white;
                gunLock_Key.color = Color.white;
            }
            else
            {
                gunLock_BG.color = initBGColor;
                gunLock_Lock.color = initLockColor;
                gunLock_Key.color = initKeyColor;
            }
        }
    } 
}