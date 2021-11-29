using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBoard : MonoBehaviour
{
    public GameObject scare1, scare2, scare3, lose, restartButton, mrBrightLife;
    public Mr_Bright mrBright;

    void Start()
    {
        scare1.SetActive(true);
        scare2.SetActive(true);
        scare3.SetActive(true);
        lose.SetActive(false);
        restartButton.SetActive(false);

        if (GameLogic.MrBrightLifes == 0)
        {
            mrBrightLife.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void UpdateBoard(int number, Maria maria)
    {
        if (number == 2){
            scare3.SetActive(false);
        }
        else if(number == 1){
            scare2.SetActive(false);
        }
        else if(number == 0){
            scare1.SetActive(false);
            GameLogic.MariaFinalScare(mrBright, maria, lose, restartButton, gameObject, mrBrightLife);
        }
    }
}
