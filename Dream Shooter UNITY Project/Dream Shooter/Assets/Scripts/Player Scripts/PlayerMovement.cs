using Assets.Scripts;
using Assets.Scripts.Player_Scripts.Shooting_Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The speed at which the player moves.
        /// </summary>
        public float movementSpeed;

        /// <summary>
        /// Return true if movement is locked, or false if not.
        /// </summary>
        public bool movementLocked { get; private set; }

        /// <summary>
        /// The controller attached to the player character.
        /// </summary>
        private CharacterController controller;
        #endregion

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            //Movement Locking Check
            movementLocked = Input.GetKey(KeyCode.N);

            MovementInput();
        }

        /// <summary>
        /// The method that's in charge of the moving input.
        /// </summary>
        private void MovementInput()
        {
            if (!movementLocked)
            {
                controller.Move(InputVectorProcessor.Generate() * movementSpeed * Time.deltaTime);
            }
        }
    } 
}