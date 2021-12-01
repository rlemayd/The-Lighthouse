using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartScene()
    {
        GameLogic.MrBrightLifes = 1;
        SceneManager.LoadScene("StartScreen");
    }
}
