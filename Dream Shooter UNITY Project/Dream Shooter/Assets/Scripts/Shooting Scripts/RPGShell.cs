using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shells that take a bit to get going, and have a blast radius.
/// </summary>
public class RPGShell : MonoBehaviour, IDamager
{
    #region MyRegion
    /// <summary>
    /// The speed the RPG Shell starts moving at.
    /// </summary>
    public float startingSpeed;
    /// <summary>
    /// The maximum speed this RPG Shell can travel.
    /// </summary>
    public float maximumSpeed;
    /// <summary>
    /// The time it takes for this RPG Shell to accelerate.
    /// </summary>
    public float accelerationTime;
    /// <summary>
    /// The curve at which this RPG Shell accelerates at.
    /// </summary>
    public AnimationCurve accelerationCurve;
    /// <summary>
    /// The blast radius of this shell when it hits something.
    /// </summary>
    public float blastRadius;
    /// <summary>
    /// The Lifetime of the bullet.
    /// </summary>
    public float bulletLife;
    /// <summary>
    /// The damage value of this bullet exposed to the Unity Editor.
    /// </summary>
    public int exposedDamageValue;
    /// <summary>
    /// The effect that plays when this RPG shell blasts.
    /// </summary>
    public GameObject blastEffect;

    /// <summary>
    /// Implemented from IDamager, the damage value of the Damager as seen by other Damagables.
    /// </summary>
    public int damageValue { get { return exposedDamageValue; } set { exposedDamageValue = value; } }

    /// <summary>
    /// The current speed of the RPG Shell.
    /// </summary>
    private float currentSpeed;
    /// <summary>
    /// A timer for the lerping of speed.
    /// </summary>
    private float timer = 0f;
    #endregion

    void Start()
    {
        Destroy(gameObject, bulletLife);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > accelerationTime)
        {
            timer = accelerationTime;
        }

        float lerpRatio = timer / accelerationTime;

        currentSpeed = Mathf.Lerp(startingSpeed, maximumSpeed, accelerationCurve.Evaluate(lerpRatio));

        transform.Translate(Time.deltaTime * currentSpeed * transform.right, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }

    /// <summary>
    /// Implemented from IDamager, the behavior of the Damager once it has damaged a Damagable.
    /// </summary>
    public void OnceDamaged()
    {
        Instantiate(blastEffect, transform.position, transform.rotation);

        Collider[] overlappedObjects = Physics.OverlapSphere(transform.position, blastRadius);
        if (overlappedObjects.Length != 0)
        {
            foreach (Collider obj in overlappedObjects)
            {
                BaseDamagable damagable = obj.GetComponent<BaseDamagable>();
                if (damagable != null)
                {
                    damagable.DamageObject(damageValue / 2);
                }
            } 
        }
        
        Destroy(gameObject);
    }
}
