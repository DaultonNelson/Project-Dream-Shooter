using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    #endregion

    private void Start()
    {
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
}