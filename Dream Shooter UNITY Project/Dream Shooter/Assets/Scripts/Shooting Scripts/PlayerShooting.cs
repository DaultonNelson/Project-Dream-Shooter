using Assets.Scripts.UI_Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting_Scripts
{
    public class PlayerShooting : MonoBehaviour
    {
        #region Variables
        //NOTE: Starting Gun only set in the Unity Editor for now
        /// <summary>
        /// The gun the player starts with.
        /// </summary>
        public GameObject startingGun;
        /// <summary>
        /// The MeshRenderer that's attached to the Player.
        /// </summary>
        public MeshRenderer playerRenderer;
        /// <summary>
        /// The Circle behind the character.
        /// </summary>
        public SpriteRenderer characterCirc;
        /// <summary>
        /// The trail that follows around the player.
        /// </summary>
        public TrailRenderer playerTrail;

        #region Gun Location Variables
        [Header("Gun Locations")]
        /// <summary>
        /// The Location where the gun will be Up.
        /// </summary>
        public Transform gunLocation_Up;
        /// <summary>
        /// The Location where the gun will be Down.
        /// </summary>
        public Transform gunLocation_Down;
        /// <summary>
        /// The Location where the gun will be Left.
        /// </summary>
        public Transform gunLocation_Left;
        /// <summary>
        /// The Location where the gun will be Right.
        /// </summary>
        public Transform gunLocation_Right;
        /// <summary>
        /// The Location where the gun will be Top Right.
        /// </summary>
        public Transform gunLocation_TopRight;
        /// <summary>
        /// The Location where the gun will be Bottom Right.
        /// </summary>
        public Transform gunLocation_BotRight;
        /// <summary>
        /// The Location where the gun will be Top Left.
        /// </summary>
        public Transform gunLocation_TopLeft;
        /// <summary>
        /// The Location where the gun will be Bottom Left.
        /// </summary>
        public Transform gunLocation_BotLeft;
        #endregion

        /// <summary>
        /// The gun the player is currently holding in their possession.
        /// </summary>
        public GameObject currentlyHeldGun { get; private set; }
        /// <summary>
        /// Return true if shooting gun is locked in place, or false if not.
        /// </summary>
        public bool gunLocked { get; private set; }

        /// <summary>
        /// The behavior attached to the player's gun.
        /// </summary>
        private PlayerGun gunBehavior;
        /// <summary>
        /// The Material attached to the player.
        /// </summary>
        private Material playerMaterial;
        /// <summary>
        /// The Scoring Manager in the scene.
        /// </summary>
        private ScoringManager scoreManager;
        /// <summary>
        /// The vector that holds the player's input information.
        /// </summary>
        private Vector3 inputVector;
        /// <summary>
        /// The time the player has to wait until they can fire a bullet again.
        /// </summary>
        private float fireWaitTime;
        #endregion

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoringManager>();
            playerMaterial = playerRenderer.material;

            ChangeGun(startingGun);
        }

        void Update()
        {
            inputVector = CalculateInputVector();

            //Gun Locking Check
            if (Input.GetKey(KeyCode.M))
            {
                gunLocked = true;
            }
            else
            {
                gunLocked = false;
            }

            ShootingInput();
            PlayerGunControl();
        }

        /// <summary>
        /// Changes the Player's gun to the given object.
        /// </summary>
        /// <param name="newGun">
        /// The new gun the player will have.
        /// </param>
        public void ChangeGun(GameObject newGun)
        {
            if (newGun == null)
            {
                Debug.LogError("No Gun Object given", gameObject);
                return;
            }

            if (currentlyHeldGun != null)
            {
                Destroy(currentlyHeldGun);
            }

            currentlyHeldGun = Instantiate(newGun, gunLocation_Right.localPosition, Quaternion.identity);
            currentlyHeldGun.transform.parent = transform;

            gunBehavior = currentlyHeldGun.GetComponent<PlayerGun>();

            playerMaterial.SetColor("_OutlineColor", gunBehavior.gunGradient.colorKeys[0].color);
            characterCirc.color = new Color(
                gunBehavior.gunGradient.colorKeys[0].color.r,
                gunBehavior.gunGradient.colorKeys[0].color.b,
                gunBehavior.gunGradient.colorKeys[0].color.g,
                characterCirc.color.a);
            playerTrail.colorGradient = gunBehavior.gunGradient;
            scoreManager.ChangeTextMeshColor(gunBehavior.gunGradient);
        }

        /// <summary>
        /// The method that's in charge of calculating the input vector.
        /// </summary>
        /// <returns>
        /// A new vector with the player's input.
        /// </returns>
        private Vector3 CalculateInputVector()
        {
            return new Vector3(
                Input.GetAxis("Horizontal"), // X
                Input.GetAxis("Vertical"),   // Y
                0f);                         // Z
        }

        /// <summary>
        /// The method that's in charge of the shooting input.
        /// </summary>
        private void ShootingInput()
        {
            if (Input.GetButton("Jump"))
            {
                if (fireWaitTime <= 0)
                {
                    gunBehavior.Shoot();
                    fireWaitTime = 1;
                }
            }

            fireWaitTime -= Time.deltaTime * gunBehavior.gunFireRate;
        }

        /// <summary>
        /// The method that's in charge of rotating the gun to the player's control.
        /// </summary>
        private void PlayerGunControl()
        {
            if (!gunLocked)
            {
                // Up
                if (inputVector.x == 0 && inputVector.y > 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_Up.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_Up.localEulerAngles;
                }
                // Down
                else if (inputVector.x == 0 && inputVector.y < 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_Down.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_Down.localEulerAngles;
                }
                // Left
                else if (inputVector.x < 0 && inputVector.y == 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_Left.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_Left.localEulerAngles;
                }
                // Right
                else if (inputVector.x > 0 && inputVector.y == 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_Right.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_Right.localEulerAngles;
                }
                // Top Right
                else if (inputVector.x > 0 && inputVector.y > 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_TopRight.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_TopRight.localEulerAngles;
                }
                // Bottom Right
                else if (inputVector.x > 0 && inputVector.y < 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_BotRight.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_BotRight.localEulerAngles;
                }
                // Top Left
                else if (inputVector.x < 0 && inputVector.y > 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_TopLeft.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_TopLeft.localEulerAngles;
                }
                // Bottom Left
                else if (inputVector.x < 0 && inputVector.y < 0)
                {
                    currentlyHeldGun.transform.localPosition = gunLocation_BotLeft.localPosition;
                    currentlyHeldGun.transform.localEulerAngles = gunLocation_BotLeft.localEulerAngles;
                }
            }
        }
    } 
}