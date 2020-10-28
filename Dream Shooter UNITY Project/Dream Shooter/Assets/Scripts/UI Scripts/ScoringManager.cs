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
        public int currentScore { get; private set; }

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
            currentScore += incomingScore;

            if (currentScore < 0)
            {
                currentScore = 0;
            }

            if (currentScore > maxScoreAllowed)
            {
                currentScore = maxScoreAllowed;
            }

            scoreValue.text = currentScore.ToString("00 000 000");
        }

        /// <summary>
        /// Changes the color of the score related Text Meshes.
        /// </summary>
        /// <param name="newGradient">
        /// The gradient the TMPs will change to.
        /// </param>
        public void ChangeTextMeshColor(Gradient newGradient)
        {
            ChangeTMPGradient(scoreLabel, newGradient.colorKeys);
            ChangeTMPGradient(scoreValue, newGradient.colorKeys);
        }

        private void ChangeTMPGradient(TMP_Text TMP, GradientColorKey[] newColors)
        {
            // color0 - Top Left
            // color1 - Top Right
            // color2 - Bottom Left
            // color3 - Bottom Right
            TMP.colorGradient = new VertexGradient(
                newColors[0].color,
                newColors[0].color,
                newColors[1].color,
                newColors[1].color);
        }
    } 
}