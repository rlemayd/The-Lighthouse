using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int score = 0;
    private void Awake()
    {
        int numberOfTrackers = FindObjectsOfType<ScoreTracker>().Length;
        if (numberOfTrackers != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
