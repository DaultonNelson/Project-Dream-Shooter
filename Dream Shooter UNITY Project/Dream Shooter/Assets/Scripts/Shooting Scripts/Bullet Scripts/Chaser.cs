using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting_Scripts.Bullet_Scripts
{
    /// <summary>
    /// The bullet that belongs to the Chaser weapon.
    /// </summary>
    public class Chaser : BaseBullet
    {
        #region Variables
        /// <summary>
        /// The speed this chaser bullet Changes to when it's chasing something.
        /// </summary>
        [Header("Chaser Bullet Variables")]
        public float chasingSpeed;
        /// <summary>
        /// The speed of this Chaser's rotation when it's chasing it's target.
        /// </summary>
        public float rotateSpeed;
        /// <summary>
        /// The range at which this Chaser bullet senses things.
        /// </summary>
        public float senseRange;
        /// <summary>
        /// The Gradient this Chaser bullet's trail changes to when chasing things.
        /// </summary>
        public Gradient chasingGradient;

        /// <summary>
        /// The object this Chaser bullet will chase.
        /// </summary>
        private GameObject objectToChase = null;
        /// <summary>
        /// The Rigidbody attached to this Chaser bullet.
        /// </summary>
        private Rigidbody attachedRigidbody;
        #endregion

        private void Start()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
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
            attachedRigidbody.velocity = transform.right * chasingSpeed;

            //To make our chaser chase it's target, we need to get the cross product of the chaser's direction and the direction of the target to get a orthogonal vector
            //An orthogonal vector sticks out from the two vectors we give it, and it's length is dependent on how close or far these vector's are

            //Getting the direction from our chaser to it's target
            Vector2 direction = objectToChase.transform.position - transform.position;

            //Normalizing our direction so it has a length of one, which we'll need for the cross productions
            direction.Normalize();

            //The direction our Chaser will always be facing: transform.right
            //Getting cross product, and storing a float so we get length of orthogonal vector and whether or not it's pointing in or out of the screen
            float rotateAmount = Vector3.Cross(direction, transform.right).z;

            //Edit our Rigidbody's angular velocity so it rotate towards the target
            attachedRigidbody.angularVelocity = new Vector3(0, 0, -rotateAmount * rotateSpeed);
        }

        /// <summary>
        /// The method in charge of the Chaser's sensing of enemy Damagables.
        /// </summary>
        private void SenseForChase()
        {
            Ray sphereCastRay = new Ray(transform.position, transform.right);

            //The info on what we hit
            RaycastHit hitInfo;

            if (Physics.SphereCast(sphereCastRay, senseRange, out hitInfo, senseRange))
            {

                if (hitInfo.collider.tag == "Enemy")
                {
                    //We're hitting an Enemy with our ray
                    Debug.DrawLine(sphereCastRay.origin, hitInfo.point, Color.green);

                    bulletTrail.colorGradient = chasingGradient;

                    objectToChase = hitInfo.collider.gameObject;
                }
            }

            //transform.Translate(Time.deltaTime * startingSpeed * transform.right, Space.World);
            attachedRigidbody.velocity = transform.right * startingSpeed;
        }

        private void OnDrawGizmos()
        {
            if (objectToChase == null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, senseRange);
            }
            else
            {
                Debug.DrawLine(transform.position, objectToChase.transform.position, Color.green);
            }
        }
    } 
}