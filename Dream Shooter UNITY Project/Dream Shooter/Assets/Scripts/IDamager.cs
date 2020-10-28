using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamager
{
    /// <summary>
    /// The amount this bullet damages a Damagable.
    /// </summary>
    float DamageValue { get; set; }

    /// <summary>
    /// Call once this damager has damaged an Damagable.
    /// </summary>
    void OnceDamaged();
}