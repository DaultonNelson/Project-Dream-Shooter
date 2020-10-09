using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The model of the player's gun.
    /// </summary>
    public Transform playerGunModel;
    /// <summary>
    /// The Nozzle the player will shoot bullets from.
    /// </summary>
    public Transform gunNozzle;
    /// <summary>
    /// The bullet prefab the player will shoot.
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// The Fire Rate of our shooting (in seconds).
    /// </summary>
    public float fireRate;

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
    /// Return true if shooting gun is locked in place, or false if not.
    /// </summary>
    public bool gunLocked { get; private set; }

    /// <summary>
    /// The vector that holds the player's input information.
    /// </summary>
    private Vector3 inputVector;
    /// <summary>
    /// The time the player has to wait until they can fire a bullet again.
    /// </summary>
    private float fireWaitTime;
    #endregion

    void Start()
    {

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
                Instantiate(bulletPrefab, gunNozzle.position, gunNozzle.rotation);
                fireWaitTime = 1;
            }
        }

        fireWaitTime -= Time.deltaTime * fireRate;
    }

    /// <summary>
    /// The method that's in charge of rotating the gun to the player's control.
    /// </summary>
    private void PlayerGunControl()
    {
        //TODO: The rules here are not consistent for some reason.
        if (!gunLocked)
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