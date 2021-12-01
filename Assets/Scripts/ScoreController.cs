using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    private int totalScore = 0;
    public Text scoreText;
    ScoreTracker tracker;

    private void Awake()
    {
        instance = this;
        tracker = FindObjectOfType<ScoreTracker>();
        if (tracker)
        {
            totalScore = tracker.score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = totalScore.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        tracker.score = totalScore;
        scoreText.text = totalScore.ToString();
    }
}
