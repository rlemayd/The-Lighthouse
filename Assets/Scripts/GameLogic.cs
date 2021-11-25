using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        private static List<string> sceneOrder = new List<string> {
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5"
        };

        public static int MrBrightLifes = 1;

        public static void MariaFinalScare(Mr_Bright mrBright, Maria maria, GameObject lose, GameObject restartButton, GameObject scareBoard, GameObject mrBrightLife)
        {
            if (MrBrightLifes > 0)
            {
                MrBrightLifes--;
                mrBrightLife.SetActive(false);
                maria.restartSustos();
                GoBackRespectiveLevel();
            }
            else
            {
                lose.SetActive(true);
                restartButton.SetActive(true);
                mrBright.DisableMrBright();
                scareBoard.SetActive(false);
            }
        }

        private static void GoBackRespectiveLevel()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if(sceneOrder.IndexOf(currentScene) <= 2)
            {
                SceneManager.LoadScene(sceneOrder[0]);
            }else
            {
                SceneManager.LoadScene(sceneOrder[sceneOrder.IndexOf(currentScene) - 2]);
            }
        }
    }
}