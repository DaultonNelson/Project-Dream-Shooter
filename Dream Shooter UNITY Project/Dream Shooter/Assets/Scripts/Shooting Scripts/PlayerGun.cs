﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The bullet prefab the player will shoot.
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// The Fire Rate of the gun (shots per second).
    /// </summary>
    public float gunFireRate;

    /// <summary>
    /// The Nozzle the player will shoot bullets from.
    /// </summary>
    public Transform gunNozzle { get; private set; }
    #endregion

    void Start()
    {
        gunNozzle = transform.GetChild(0);
    }

    /// <summary>
    /// Shoots the gun.
    /// </summary>
    public void Shoot()
    {
        //TODO: Add optional opposite force to player once they shoot, lick kick-back
        Instantiate(bulletPrefab, gunNozzle.position, gunNozzle.rotation);
    }
}