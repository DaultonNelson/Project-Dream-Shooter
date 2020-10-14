using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The cases the player runs into to Swap Weapons
/// </summary>
public class GunCase : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The Gun held in this case.
    /// </summary>
    public GameObject encasedGun;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerShooting playShoot = other.GetComponent<PlayerShooting>();
            playShoot.ChangeGun(encasedGun);
            Destroy(gameObject);
        }
    }
}