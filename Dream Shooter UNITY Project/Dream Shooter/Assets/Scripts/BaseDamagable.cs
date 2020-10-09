using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamagable : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The current health value of this Damagable.
    /// </summary>
    public int currHealthValue;
    /// <summary>
    /// The tag the colliding object needs to have to damage this Damagable
    /// </summary>
    public string damagingTag;
    /// <summary>
    /// Return true if this instance of the script should ignore the shaking code, or false if not.
    /// </summary>
    public bool ignoreShake;

    /// <summary>
    /// The shaking script attached to this GameObject.
    /// </summary>
    private ShakeTransformS shake;
    #endregion

    private void Start()
    {
        if (!ignoreShake)
        {
            shake = GetComponent<ShakeTransformS>(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == damagingTag)
        {
            IDamager damager = other.GetComponent<IDamager>();

            if (damager != null)
            {
                if (!ignoreShake)
                {
                    shake.Begin(); 
                }

                currHealthValue -= damager.damageValue;

                damager.OnceDamaged();

                DeathCheck();
            }
        }
    }

    /// <summary>
    /// The method that's in charge of the Damagable's death.
    /// </summary>
    private void DeathCheck()
    {
        if (currHealthValue <= 0)
        {
            Destroy(gameObject);
        }
    }
}