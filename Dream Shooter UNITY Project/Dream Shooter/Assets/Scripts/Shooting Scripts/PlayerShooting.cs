using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    #region Variables
    //TODO: Rethink setting this as a prefab, as it could be Destroyed
    /// <summary>
    /// The gun the player has in their possession.
    /// </summary>
    public GameObject gunObject;

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
    /// The behavior attached to the player's gun.
    /// </summary>
    private PlayerGun gunBehavior;
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
        ChangeGun(gunObject);
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

    //TODO: Test Weapon Swapping   
    /// <summary>
    /// Changes the Player's gun to the given object.
    /// </summary>
    /// <param name="newGun">
    /// The new gun the player will have.
    /// </param>
    private void ChangeGun(GameObject newGun)
    {
        if (newGun == null)
        {
            Debug.LogWarning("No Gun Object given");
            return;
        }
        if (gunObject != null)
        {
            Destroy(gunObject);
        }

        gunBehavior = newGun.GetComponent<PlayerGun>();
        if (gunBehavior != null)
        {
            gunObject = Instantiate(newGun, gunLocation_Right.localPosition, Quaternion.identity);
            gunObject.transform.parent = transform;
        }
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

        fireWaitTime -= Time.deltaTime * gunBehavior.fireRate;
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
                gunObject.transform.localPosition = gunLocation_Up.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_Up.localEulerAngles;
            }
            // Down
            else if (inputVector.x == 0 && inputVector.y < 0)
            {
                gunObject.transform.localPosition = gunLocation_Down.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_Down.localEulerAngles;
            }
            // Left
            else if (inputVector.x < 0 && inputVector.y == 0)
            {
                gunObject.transform.localPosition = gunLocation_Left.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_Left.localEulerAngles;
            }
            // Right
            else if (inputVector.x > 0 && inputVector.y == 0)
            {
                gunObject.transform.localPosition = gunLocation_Right.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_Right.localEulerAngles;
            }
            // Top Right
            else if (inputVector.x > 0 && inputVector.y > 0)
            {
                gunObject.transform.localPosition = gunLocation_TopRight.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_TopRight.localEulerAngles;
            }
            // Bottom Right
            else if (inputVector.x > 0 && inputVector.y < 0)
            {
                gunObject.transform.localPosition = gunLocation_BotRight.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_BotRight.localEulerAngles;
            }
            // Top Left
            else if (inputVector.x < 0 && inputVector.y > 0)
            {
                gunObject.transform.localPosition = gunLocation_TopLeft.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_TopLeft.localEulerAngles;
            }
            // Bottom Left
            else if (inputVector.x < 0 && inputVector.y < 0)
            {
                gunObject.transform.localPosition = gunLocation_BotLeft.localPosition;
                gunObject.transform.localEulerAngles = gunLocation_BotLeft.localEulerAngles;
            }
            // None of the Above
            else
            {
                Debug.LogWarning("None of the above", gameObject);
            }
        }
    }
}