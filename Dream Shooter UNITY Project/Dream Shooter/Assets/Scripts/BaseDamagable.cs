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
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == damagingTag)
        {
            currHealthValue--;

            DeathCheck();
        }
    }

    private void DeathCheck()
    {
        if (currHealthValue <= 0)
        {
            Destroy(gameObject);
        }
    }
}