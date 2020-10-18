using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Automatically destroy the object after n seconds.
/// </summary>
public class AutoDestroy : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The Life Time of the Object.
    /// </summary>
    public float lifeTime;
    #endregion

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}