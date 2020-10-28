using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts
{
    public class PlayerVisuals : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The transform of the player model object.
        /// </summary>
        public Transform playerCharacterModel;
        /// <summary>
        /// The MeshRenderer that's attached to the Player.
        /// </summary>
        public MeshRenderer playerRenderer;
        /// <summary>
        /// The Circle behind the character.
        /// </summary>
        public SpriteRenderer characterCirc;
        /// <summary>
        /// The trail that follows around the player.
        /// </summary>
        public TrailRenderer playerTrail;

        /// <summary>
        /// The Material attached to the player.
        /// </summary>
        private Material playerMaterial;
        /// <summary>
        /// The Player Overhead Component container.
        /// </summary>
        private PlayerOverhead playerOverhead;
        #endregion

        private void Awake()
        {
            playerOverhead = GetComponent<PlayerOverhead>();
            playerMaterial = playerRenderer.material;
        }

        private void Update()
        {
            GunAndModelLocking();
        }

        /// <summary>
        /// This method locks the player's gun and character model.
        /// </summary>
        private void GunAndModelLocking()
        {
            if (playerOverhead.shooting.gunLocked)
            {
                playerCharacterModel.localScale = new Vector3(
                   Mathf.Sign(InputVectorProcessor.Generate().x) * Mathf.Abs(playerCharacterModel.localScale.x),
                   playerCharacterModel.localScale.y,
                   playerCharacterModel.localScale.z);
            }
        }

        /// <summary>
        /// Changes the the player visual gradients.
        /// </summary>
        /// <param name="newGradient">
        /// The new gradient that will make up the visuals.
        /// </param>
        public void ChangeVisualsGradient(Gradient newGradient)
        {
            playerMaterial.SetColor("_OutlineColor", newGradient.colorKeys[0].color);
            characterCirc.color = new Color(
                newGradient.colorKeys[0].color.r,
                newGradient.colorKeys[0].color.b,
                newGradient.colorKeys[0].color.g,
                characterCirc.color.a);
            playerTrail.colorGradient = newGradient;
        }
    }
}
