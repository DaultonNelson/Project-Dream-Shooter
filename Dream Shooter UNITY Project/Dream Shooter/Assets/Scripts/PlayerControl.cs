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
    /// The controller attached to the player character.
    /// </summary>
    private CharacterController controller;

    /// <summary>
    /// The Nozzle the player will shoot bullets from.
    /// </summary>
    [Header("Shooting Variables")]
    public Transform gunNozzle;
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
        PlayerShooting();
        //TODO: Implement movement
        PlayerMovement();
        //TODO: Implement angling the gun
    }

    /// <summary>
    /// The method that'sin charge of moving.
    /// </summary>
    private void PlayerMovement()
    {
        Vector3 movementVector = new Vector3 (
            Input.GetAxis("Horizontal"), // X
            Input.GetAxis("Vertical"),   // Y
            0f);                         // Z

        controller.Move(movementVector * movementSpeed * Time.deltaTime);
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
}