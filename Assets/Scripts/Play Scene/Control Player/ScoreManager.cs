using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{    
    private int score;
    public int jumlahMusuh;
    public TMP_Text scoreTextRealTime;
    public TMP_Text scoreTextFinish;

    // Update is called once per frame
    public void AddScore(int points)
    {
        score += points;
        scoreTextRealTime.text = score + "/" + jumlahMusuh;
        scoreTextFinish.text = score + "/" + jumlahMusuh;
    }
}