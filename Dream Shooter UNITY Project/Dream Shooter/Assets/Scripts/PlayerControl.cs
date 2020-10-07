using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The speed at which the player moves.
    /// </summary>
    [Header("Movement Variables")]
    public float movementSpeed;
    /// <summary>
    /// The transform of the player model object.
    /// </summary>
    public Transform playerCharacterModel;

    /// <summary>
    /// The controller attached to the player character.
    /// </summary>
    private CharacterController controller;
    /// <summary>
    /// The vector that holds the player's input information.
    /// </summary>
    private Vector3 inputVector;

    /// <summary>
    /// The model of the player's gun.
    /// </summary>
    [Header("Shooting Variables")]
    public Transform playerGunModel;
    /// <summary>
    /// The Nozzle the player will shoot bullets from.
    /// </summary>
    public Transform gunNozzle;

    #region Gun Location Variables
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
    /// The bullet prefab the player will shoot.
    /// </summary>
    public GameObject bulletPrefab;
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        inputVector = CalculateinputVector();
        PlayerMovement();

        PlayerShooting();
        PlayerGunControl();
    }

    /// <summary>
    /// The method that's in charge of calculating the input vector.
    /// </summary>
    /// <returns>
    /// A new vector with the player's input.
    /// </returns>
    private Vector3 CalculateinputVector()
    {
        return new Vector3(
            Input.GetAxis("Horizontal"), // X
            Input.GetAxis("Vertical"),   // Y
            0f);                         // Z
    }

    /// <summary>
    /// The method that's in charge of moving.
    /// </summary>
    private void PlayerMovement()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            playerCharacterModel.localScale = new Vector3(
                Mathf.Sign(inputVector.x) * Mathf.Abs(playerCharacterModel.localScale.x),
                playerCharacterModel.localScale.y,
                playerCharacterModel.localScale.z); 
        }

        controller.Move(inputVector * movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// The method that's in charge of shooting.
    /// </summary>
    private void PlayerShooting()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(bulletPrefab, gunNozzle.position, gunNozzle.rotation);
        }
    }

    /// <summary>
    /// The method that's in charge of rotating the gun to the player's control.
    /// </summary>
    private void PlayerGunControl()
    {
        //TODO: Implement angling the gun
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            // Up
            if (inputVector.x == 0 && inputVector.y > 0)
            {
                playerGunModel.localPosition = gunLocation_Up.localPosition;
                playerGunModel.localEulerAngles = gunLocation_Up.localEulerAngles;
            }
            // Down
            else if (inputVector.x == 0 && inputVector.y < 0)
            {
                playerGunModel.localPosition = gunLocation_Down.localPosition;
                playerGunModel.localEulerAngles = gunLocation_Down.localEulerAngles;
            }
            // Left
            else if (inputVector.x < 0 && inputVector.y == 0)
            {
                playerGunModel.localPosition = gunLocation_Left.localPosition;
                playerGunModel.localEulerAngles = gunLocation_Left.localEulerAngles;
            }
            // Right
            else if (inputVector.x > 0 && inputVector.y == 0)
            {
                playerGunModel.localPosition = gunLocation_Right.localPosition;
                playerGunModel.localEulerAngles = gunLocation_Right.localEulerAngles;
            }
            // Top Right
            else if (inputVector.x > 0 && inputVector.y > 0)
            {
                playerGunModel.localPosition = gunLocation_TopRight.localPosition;
                playerGunModel.localEulerAngles = gunLocation_TopRight.localEulerAngles;
            }
            // Bottom Right
            else if (inputVector.x > 0 && inputVector.y < 0)
            {
                playerGunModel.localPosition = gunLocation_BotRight.localPosition;
                playerGunModel.localEulerAngles = gunLocation_BotRight.localEulerAngles;
            }
            // Top Left
            else if (inputVector.x < 0 && inputVector.y > 0)
            {
                playerGunModel.localPosition = gunLocation_TopLeft.localPosition;
                playerGunModel.localEulerAngles = gunLocation_TopLeft.localEulerAngles;
            }
            // Bottom Left
            else if (inputVector.x < 0 && inputVector.y < 0)
            {
                playerGunModel.localPosition = gunLocation_BotLeft.localPosition;
                playerGunModel.localEulerAngles = gunLocation_BotLeft.localEulerAngles;
            }
            // None of the Above
            else
            {
                Debug.LogWarning("None of the above", gameObject);
            }
        }
    }
}