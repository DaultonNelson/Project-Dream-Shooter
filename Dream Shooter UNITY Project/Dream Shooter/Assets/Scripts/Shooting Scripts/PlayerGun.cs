using System.Collections;
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
    /// The Fire Rate of our shooting (in seconds).
    /// </summary>
    public float fireRate;
    
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
        Instantiate(bulletPrefab, gunNozzle.position, gunNozzle.rotation);
    }
}