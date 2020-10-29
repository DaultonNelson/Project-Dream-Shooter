using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Shooting_Scripts.Bullet_Scripts
{
    /// <summary>
    /// The base class all bullets inherit from.
    /// </summary>
    public class BaseBullet : MonoBehaviour, IDamager
    {
        #region Variables
        /// <summary>
        /// The starting speed of the bullet.
        /// </summary>
        [Header("Base Bullet Variables")]
        public float startingSpeed;
        /// <summary>
        /// The lifetime of the bullet.
        /// </summary>
        public float bulletLife;
        /// <summary>
        /// The damage value of this bullet exposed to the Unity Editor.
        /// </summary>
        public float exposedDamageValue;

        /// <summary>
        /// The TrailRenderer that's attached to this bullet.
        /// </summary>
        public TrailRenderer bulletTrail { get; private set; }
        /// <summary>
        /// Implemented from IDamager, the damage value of the Damager as seen by other Damagables.
        /// </summary>
        public float DamageValue { get { return exposedDamageValue; } set { exposedDamageValue = value; } }
        #endregion

        private void Awake()
        {
            bulletTrail = GetComponent<TrailRenderer>();

            Destroy(gameObject, bulletLife);
        }

        /// <summary>
        /// Implemented from IDamager, the behavior of the Damager once it has damaged a Damagable.
        /// </summary>
        public virtual void OnceDamaged()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Overrides the Properties of the Base Bullet.
        /// </summary>
        /// <param name="_speed">
        /// The bullet's new speed.
        /// </param>
        /// <param name="_life">
        /// The bullet's new lifetime.
        /// </param>
        public void OverrideProperties(float _speed, float _life)
        {
            startingSpeed = _speed;
            bulletLife = _life;
        }
    } 
}