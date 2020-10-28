using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts
{
    public class InputVectorProcessor
    {
        /// <summary>
        /// Generates a new Vector2 based off the player's input.
        /// </summary>
        /// <returns>
        /// A new vector with the player's input.
        /// </returns>
        public static Vector2 Generate()
        {
            Vector2 output = new Vector2(
                Input.GetAxis("Horizontal"), // X
                Input.GetAxis("Vertical"));

            return output;
        }
    }
}
