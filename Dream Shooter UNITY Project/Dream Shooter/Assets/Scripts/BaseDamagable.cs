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
    /// The score value of this Damagable if it's destroyed.
    /// </summary>
    public int scoreValue;
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
    /// <summary>
    /// The scoring manager in the scene.
    /// </summary>
    private ScoringManager scoreManager;
    #endregion

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoringManager>();
        if (scoreManager == null)
        {
            Debug.LogError("Scoring Manager not found in scene!", gameObject);
        }

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
                damager.OnceDamaged();

                DamageObject(damager.damageValue);
            }
        }
    }

    /// <summary>
    /// Damages the Damagable.
    /// </summary>
    /// <param name="incomingDamage">
    /// The incoming damage.
    /// </param>
    public void DamageObject(int incomingDamage)
    {
        if (!ignoreShake)
        {
            shake.Begin();
        }

        currHealthValue -= incomingDamage;

        DeathCheck();
    }

    /// <summary>
    /// The method that's in charge of the Damagable's death.
    /// </summary>
    private void DeathCheck()
    {
        if (currHealthValue <= 0)
        {
            scoreManager.CalculateScore(scoreValue);

            Destroy(gameObject);
        }
    }
}