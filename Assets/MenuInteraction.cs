using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            SceneManager.LoadScene("StartScreen");
            ScoreTracker tracker = FindObjectOfType<ScoreTracker>();
            if (tracker)
            {
                Destroy(tracker);
            }
        }   
    }
}
