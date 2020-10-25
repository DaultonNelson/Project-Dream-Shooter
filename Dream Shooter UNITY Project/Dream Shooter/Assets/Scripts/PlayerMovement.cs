using Assets.Scripts.Shooting_Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The speed at which the player moves.
    /// </summary>
    public float movementSpeed;
    /// <summary>
    /// The transform of the player model object.
    /// </summary>
    public Transform playerCharacterModel;

    /// <summary>
    /// Return true if movement is locked, or false if not.
    /// </summary>
    public bool movementLocked { get; private set; }

    /// <summary>
    /// The controller attached to the player character.
    /// </summary>
    private CharacterController controller;
    /// <summary>
    /// The shooting script attached to the player.
    /// </summary>
    private PlayerShooting shooting;
    /// <summary>
    /// The vector that holds the player's input information.
    /// </summary>
    private Vector3 inputVector;
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        shooting = GetComponent<PlayerShooting>();
    }

    void Update()
    {
        inputVector = CalculateInputVector();

        //Movement Locking Check
        if (Input.GetKey(KeyCode.N))
        {
            movementLocked = true;
        }
        else
        {
            movementLocked = false;
        }

        MovementInput();
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
    /// The method that's in charge of the moving input.
    /// </summary>
    private void MovementInput()
    {
        if (!shooting.gunLocked)
        {
            playerCharacterModel.localScale = new Vector3(
               Mathf.Sign(inputVector.x) * Mathf.Abs(playerCharacterModel.localScale.x),
               playerCharacterModel.localScale.y,
               playerCharacterModel.localScale.z); 
        }

        if (!movementLocked)
        {
            controller.Move(inputVector * movementSpeed * Time.deltaTime);
        }
    }
}