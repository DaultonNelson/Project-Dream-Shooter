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
    /// The effect that plays once the gun fires.
    /// </summary>
    public GameObject fireEffect;
    /// <summary>
    /// The color Gradient associated with this gun.
    /// </summary>
    public Gradient gunGradient;
    /// <summary>
    /// The Fire Rate of the gun (shots per second).
    /// </summary>
    public float gunFireRate;

    /// <summary>
    /// The Nozzle the player will shoot bullets from.
    /// </summary>
    public List<Transform> gunNozzles = new List<Transform>();
    /// <summary>
    /// The current nozzle this gun is firing from.
    /// </summary>
    private int currentNozzle = 0;
    #endregion

    /// <summary>
    /// Shoots the gun.
    /// </summary>
    public void Shoot()
    {
        Instantiate(bulletPrefab, gunNozzles[currentNozzle].position, gunNozzles[currentNozzle].rotation);

        if (fireEffect != null)
        {
            Instantiate(fireEffect, gunNozzles[currentNozzle].position, gunNozzles[currentNozzle].rotation); 
        }

        currentNozzle++;
        if (currentNozzle > gunNozzles.Count - 1)
        {
            currentNozzle = 0;
        }

        //HACK: Breakpoint for testing whenever a gun shoots.
        //throw new System.NullReferenceException();
    }
}