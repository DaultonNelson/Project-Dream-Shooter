using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Shooting_Scripts
{
    public class PlayerGunPlacementManager : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The Location where the gun will be Up.
        /// </summary>
        public Transform up;
        /// <summary>
        /// The Location where the gun will be Down.
        /// </summary>
        public Transform down;
        /// <summary>
        /// The Location where the gun will be Left.
        /// </summary>
        public Transform left;
        /// <summary>
        /// The Location where the gun will be Right.
        /// </summary>
        public Transform right;
        /// <summary>
        /// The Location where the gun will be Top Right.
        /// </summary>
        public Transform topRight;
        /// <summary>
        /// The Location where the gun will be Bottom Right.
        /// </summary>
        public Transform botRight;
        /// <summary>
        /// The Location where the gun will be Top Left.
        /// </summary>
        public Transform topLeft;
        /// <summary>
        /// The Location where the gun will be Bottom Left.
        /// </summary>
        public Transform botLeft;

        /// <summary>
        /// The Player Overhead Component container.
        /// </summary>
        private PlayerOverhead playerOverhead;
        #endregion

        private void Awake()
        {
            playerOverhead = GetComponent<PlayerOverhead>();
        }

        private void Update()
        {
            PlayerGunControl();
        }

        /// <summary>
        /// The method that's in charge of rotating the gun to the player's control.
        /// </summary>
        private void PlayerGunControl()
        {
            Vector2 inputVector = InputVectorProcessor.Generate();

            if (!playerOverhead.shooting.gunLocked)
            {
                // Up
                if (inputVector.x == 0 && inputVector.y > 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = up.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = up.localEulerAngles;
                }
                // Down
                else if (inputVector.x == 0 && inputVector.y < 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = down.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = down.localEulerAngles;
                }
                // Left
                else if (inputVector.x < 0 && inputVector.y == 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = left.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = left.localEulerAngles;
                }
                // Right
                else if (inputVector.x > 0 && inputVector.y == 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = right.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = right.localEulerAngles;
                }
                // Top Right
                else if (inputVector.x > 0 && inputVector.y > 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = topRight.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = topRight.localEulerAngles;
                }
                // Bottom Right
                else if (inputVector.x > 0 && inputVector.y < 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = botRight.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = botRight.localEulerAngles;
                }
                // Top Left
                else if (inputVector.x < 0 && inputVector.y > 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = topLeft.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = topLeft.localEulerAngles;
                }
                // Bottom Left
                else if (inputVector.x < 0 && inputVector.y < 0)
                {
                    playerOverhead.shooting.currentlyHeldGun.transform.localPosition = botLeft.localPosition;
                    playerOverhead.shooting.currentlyHeldGun.transform.localEulerAngles = botLeft.localEulerAngles;
                }
            }
        }
    }
}