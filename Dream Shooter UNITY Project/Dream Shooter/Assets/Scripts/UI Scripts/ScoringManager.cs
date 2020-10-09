using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: Implement Scoring System
public class ScoringManager : MonoBehaviour
{
    public TMP_Text scoreValue;

    private void Start()
    {
        scoreValue.text = Random.Range(0, 10000001).ToString("00 000 000");
    }
}