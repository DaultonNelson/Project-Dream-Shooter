using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting_Scripts.Bullet_Scripts
{
    /// <summary>
    /// Bullets that fly straight in the direction their given, no other distinct characteristics
    /// </summary>
    public class SimpleBullet : BaseBullet
    {
        private void Start()
        {
            bulletTrail.colorGradient = FindObjectOfType<PlayerGun>().gunGradient;
        }

        private void Update()
        {
            transform.Translate(Time.deltaTime * startingSpeed * transform.right, Space.World);
        }
    } 
}