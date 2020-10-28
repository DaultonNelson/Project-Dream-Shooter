using Assets.Scripts.UI_Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Shooting_Scripts
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
        /// The Player Overhead Component container.
        /// </summary>
        private PlayerOverhead playerOverhead;
        /// <summary>
        /// The Scoring Manager in the scene.
        /// </summary>
        private ScoringManager scoreManager;
        /// <summary>
        /// The time the player has to wait until they can fire a bullet again.
        /// </summary>
        private float fireWaitTime;
        #endregion

        private void Start()
        {
            playerOverhead = GetComponent<PlayerOverhead>();
            scoreManager = FindObjectOfType<ScoringManager>();

            ChangeGun(startingGun);
        }

        void Update()
        {
            //Gun Locking Check
            gunLocked = Input.GetKey(KeyCode.M);

            ShootingInput();
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

            currentlyHeldGun = Instantiate(newGun, playerOverhead.placementManager.right.localPosition, Quaternion.identity);
            currentlyHeldGun.transform.parent = transform;

            gunBehavior = currentlyHeldGun.GetComponent<PlayerGun>();

            playerOverhead.visuals.ChangeVisualsGradient(gunBehavior.gunGradient);

            scoreManager.ChangeTextMeshColor(gunBehavior.gunGradient);
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
    } 
}