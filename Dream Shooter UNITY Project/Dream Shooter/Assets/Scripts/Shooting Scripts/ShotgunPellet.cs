using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The pellet the Shotgun shoots out, spawns several smaller simple bullets at random rotations.
/// </summary>
public class ShotgunPellet : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// A reference to the simple bullet.
    /// </summary>
    public GameObject simpleBulletPrefab;
    /// <summary>
    /// The amount of spawn this pellet will instantiate.
    /// </summary>
    public int pelletSpawnSize;
    /// <summary>
    /// The angle the spawns can rotate at randomly.
    /// </summary>
    public float randomAngleLimit;


    /// <summary>
    /// The value that will override the spawns's movement speed.
    /// </summary>
    [Header("Override Bullet Variables")]
    public float bulletMoveSpeedOverride;
    /// <summary>
    /// The value that will override the spawn's lifetime.
    /// </summary>
    public float bulletLifeOverride = 1;
    #endregion

    private void Start()
    {
        for (int i = 0; i < pelletSpawnSize; i++)
        {
            GameObject spawnedBullet = Instantiate(simpleBulletPrefab, transform.position, transform.rotation);

            spawnedBullet.transform.localScale /= 2;
            if (i != 0)
            {
                spawnedBullet.transform.localEulerAngles = new Vector3
                        (spawnedBullet.transform.localEulerAngles.x,
                         spawnedBullet.transform.localEulerAngles.y,
                         spawnedBullet.transform.localEulerAngles.z + UnityEngine.Random.Range(-randomAngleLimit, randomAngleLimit));
            }

            SimpleBullet spawnedBehavior = spawnedBullet.GetComponent<SimpleBullet>();
            spawnedBehavior.bulletLife = bulletLifeOverride;
            spawnedBehavior.moveSpeed = bulletMoveSpeedOverride;
        }
        
        //HACK: Breakpoint for testing spawn's random rotation
        //throw new NullReferenceException();

        Destroy(gameObject);
    }
}