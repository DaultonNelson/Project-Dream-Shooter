using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI_Scripts
{
    public class ScoringManager : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The TMP that represents the value of the player's score.
        /// </summary>
        public TMP_Text scoreValue;

        /// <summary>
        /// The current score the player has.
        /// </summary>
        public int currScore { get; private set; }

        /// <summary>
        /// The maximum score allowed before the counter overflows.
        /// </summary>
        private const int maxScoreAllowed = 99999999;

        /// <summary>
        /// The TMP that displays the score label.
        /// </summary>
        private TMP_Text scoreLabel;
        #endregion

        private void Start()
        {
            scoreLabel = GetComponent<TMP_Text>();
            scoreValue.text = 0.ToString("00 000 000");
        }

        /// <summary>
        /// The method in charge of calculating the game score.
        /// </summary>
        /// <param name="incomingScore">
        /// The incoming score.
        /// </param>
        public void CalculateScore(int incomingScore)
        {
            currScore += incomingScore;
            if (currScore > maxScoreAllowed)
            {
                currScore = maxScoreAllowed;
            }

            scoreValue.text = currScore.ToString("00 000 000");
        }

        /// <summary>
        /// Changes the color of the score related Text Meshes.
        /// </summary>
        /// <param name="newGradient">
        /// The gradient the TMPs will change to.
        /// </param>
        public void ChangeTextMeshColor(Gradient newGradient)
        {
            scoreLabel.colorGradient = new VertexGradient(
                newGradient.colorKeys[0].color,
                newGradient.colorKeys[0].color,
                newGradient.colorKeys[1].color,
                newGradient.colorKeys[1].color);

            // color0 - Top Left
            // color1 - Top Right
            // color2 - Bottom Left
            // color3 - Bottom Right
            scoreValue.colorGradient = new VertexGradient(
                newGradient.colorKeys[0].color,
                newGradient.colorKeys[0].color,
                newGradient.colorKeys[1].color,
                newGradient.colorKeys[1].color);
        }
    } 
}