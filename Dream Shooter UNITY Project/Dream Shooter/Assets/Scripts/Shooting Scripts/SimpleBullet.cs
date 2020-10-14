using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour, IDamager
{
    #region Variables
    /// <summary>
    /// The speed at which the bullet moves.
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// The damage value of this bullet exposed to the Unity Editor.
    /// </summary>
    public int exposedDamageValue;
    /// <summary>
    /// How long the bullet floats in space in seconds before destroying itself.
    /// </summary>
    public float bulletLife = 1;
    
    /// <summary>
    /// Implemented from IDamager, the damage value of the Damager as seen by other Damagables.
    /// </summary>
    public int damageValue { get { return exposedDamageValue; } set { exposedDamageValue = value; } }
    #endregion

    private void Start()
    {
        Destroy(gameObject, bulletLife);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * transform.right, Space.World);
        Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.red);
    }
    
    /// <summary>
    /// Implemented from IDamager, the behavior of the Damager once it has damaged a Damagable.
    /// </summary>
    public void OnceDamaged()
    {
        Destroy(gameObject);
    }
}