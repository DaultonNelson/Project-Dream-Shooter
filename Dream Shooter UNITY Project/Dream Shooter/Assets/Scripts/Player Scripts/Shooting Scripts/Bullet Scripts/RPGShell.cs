using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Shooting_Scripts.Bullet_Scripts
{
    /// <summary>
    /// Shells that take a bit to get going, and have a blast radius.
    /// </summary>
    public class RPGShell : BaseBullet
    {
        #region Variables
        /// <summary>
        /// The maximum speed this RPG Shell can travel.
        /// </summary>
        [Header("RPG Shell Variables")]
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
        /// The effect that plays when this RPG shell blasts.
        /// </summary>
        public GameObject blastEffect;

        /// <summary>
        /// The current speed of the RPG Shell.
        /// </summary>
        private float currentSpeed;
        /// <summary>
        /// A timer for the lerping of speed.
        /// </summary>
        private float timer = 0f;
        #endregion

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
        public override void OnceDamaged()
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
                        damagable.DamageObject(exposedDamageValue / 2);
                    }
                }
            }

            base.OnceDamaged();
        }
    } 
}
