using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int score = 0;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
