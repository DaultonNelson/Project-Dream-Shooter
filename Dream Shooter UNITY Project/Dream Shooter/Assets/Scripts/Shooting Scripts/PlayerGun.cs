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
    /// The Fire Rate of the gun (shots per second).
    /// </summary>
    public float gunFireRate;
    
    /// <summary>
    /// Return true if this gun can override it's bullet's specs, or false if not.
    /// </summary>
    [Header("Bullet Variables")]
    public bool overrideBulletSpecs = false;
    /// <summary>
    /// The speed at which this gun's bullet moves.
    /// </summary>
    public float bulletMoveSpeed;
    /// <summary>
    /// The damage value the bullets of this gun will deal.
    /// </summary>
    public int exposedbulletDamageValue;
    /// <summary>
    /// How long this gun's bullets float in space in seconds before destroying themselves.
    /// </summary>
    public float bulletLife;

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
        GameObject spawnedBullet = Instantiate(bulletPrefab, gunNozzle.position, gunNozzle.rotation);

        if (overrideBulletSpecs)
        {
            SimpleBullet bulletBehavior = spawnedBullet.GetComponent<SimpleBullet>();

            bulletBehavior.moveSpeed = bulletMoveSpeed;
            bulletBehavior.exposedDamageValue = exposedbulletDamageValue;
            bulletBehavior.bulletLife = bulletLife; 
        }
    }
}