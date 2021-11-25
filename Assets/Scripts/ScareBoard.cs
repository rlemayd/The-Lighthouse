using Assets.Scripts;
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
        RectTransform rt = (RectTransform)transform;
        RectTransform rt2 = (RectTransform)mrBrightLife.transform;
        Debug.Log(rt.rect.width);
        if (GameLogic.MrBrightLifes == 0)
        {
            mrBrightLife.SetActive(false);
            Vector3 position = transform.position;
            //TODO; Poner la posicion proporcinal de las vidas de maria
            //position.x = mrBrightLife.transform.position.x + (rt2.rect.width/rt.rect.width);
            position.x = mrBrightLife.transform.position.x;
            transform.position = position;
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
