using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The bullet that belongs to the Chaser weapon.
/// </summary>
public class Chaser : MonoBehaviour, IDamager
{
    #region Variables
    /// <summary>
    /// The speed this Chaser bullet starts at.
    /// </summary>
    public float startingSpeed;
    /// <summary>
    /// The speed this chaser bullet Changes to when it's chasing something.
    /// </summary>
    public float chasingSpeed;
    /// <summary>
    /// The range at which this Chaser bullet senses things.
    /// </summary>
    public float senseRange;
    /// <summary>
    /// The lifetime of this Chaser bullet.
    /// </summary>
    public float chaserBulletLife;
    /// <summary>
    /// The damage value of this bullet exposed to the Unity Editor.
    /// </summary>
    public int exposedDamageValue;
    /// <summary>
    /// The Gradient this Chaser bullet's trail changes to when chasing things.
    /// </summary>
    public Gradient chasingGradient;

    /// <summary>
    /// The object this Chaser bullet will chase.
    /// </summary>
    public GameObject objectToChase = null;

    /// <summary>
    /// The TrailRenderer that's attached to this Chaser bullet.
    /// </summary>
    private TrailRenderer chaserTrail;
    /// <summary>
    /// The LineRnderer that acts as the Chaser bullet's visual sensor.
    /// </summary>
    private LineRenderer chaserLine;

    /// <summary>
    /// Implemented from IDamager, the damage value of the Damager as seen by other Damagables.
    /// </summary>
    public int damageValue { get { return exposedDamageValue; } set { exposedDamageValue = value; } }
    #endregion

    private void Start()
    {
        chaserTrail = GetComponent<TrailRenderer>();
        chaserLine = GetComponent<LineRenderer>();

        Destroy(gameObject, chaserBulletLife);
    }

    private void Update()
    {
        if (objectToChase == null)
        {
            SenseForChase();
        }
        else
        {
            ChaseObject();
        }
    }

    /// <summary>
    /// The method in charge of chasing an object.
    /// </summary>
    private void ChaseObject()
    {
        chaserLine.SetPosition(0, transform.position);
        chaserLine.SetPosition(1, objectToChase.transform.position);

        //TODO: Chase Object
    }

    /// <summary>
    /// The method in charge of the Chaser's sensing of enemy Damagables.
    /// </summary>
    private void SenseForChase()
    {
        Ray ray = new Ray(transform.position, transform.right);
        //The info on what we hit
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, senseRange))
        {
            //We're hitting something with our ray
            Debug.DrawLine(ray.origin, hitInfo.point, Color.green);

            if (hitInfo.collider.tag == "Enemy")
            {
                Debug.Log(name + " Sensed an Enemy", gameObject);

                chaserTrail.colorGradient = chasingGradient;
                chaserLine.colorGradient.SetKeys(chasingGradient.colorKeys, chaserLine.colorGradient.alphaKeys);

                objectToChase = hitInfo.collider.gameObject;
            }
        }
        else
        {
            //We're not hitting anything with our ray
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * senseRange, Color.red);
            
            chaserLine.SetPosition(0, transform.position);
            chaserLine.SetPosition(1, transform.position + transform.right * senseRange);

            transform.Translate(Time.deltaTime * startingSpeed * transform.right, Space.World);

        }
    }

    /// <summary>
    /// Implemented from IDamager, the behavior of the Damager once it has damaged a Damagable.
    /// </summary>
    public void OnceDamaged()
    {
        Destroy(gameObject);
    }
}