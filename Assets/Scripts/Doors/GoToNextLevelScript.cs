using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevelScript : MonoBehaviour
{
    private List<string> sceneOrder = new List<string> {
        "Level 1",
        "Level 2",
        "Level 3",
        "Level 4",
        "Level 5"
    };

    public void GoToNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene != sceneOrder.Last())
        {
            SceneManager.LoadScene(sceneOrder[sceneOrder.IndexOf(currentScene) + 1]);
        }
        else
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
