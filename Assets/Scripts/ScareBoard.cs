using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBoard : MonoBehaviour
{
    public GameObject scare1;
    public GameObject scare2;
    public GameObject scare3;
    public GameObject lose;
    //public GameObject luzTablero;
    //Light2D luz;
    // Start is called before the first frame update
    void Start()
    {
        scare1.SetActive(true);
        scare2.SetActive(true);
        scare3.SetActive(true);
        lose.SetActive(false);
        //luz = luzTablero.GetComponent<Light2D>();
    }

    // Update is called once per frame
    public void UpdateBoard(int number)
    {
        if (number == 2){
            scare3.SetActive(false);
        }
        else if(number == 1){
            scare2.SetActive(false);
        }
        else if(number == 0){
            scare1.SetActive(false);
            lose.SetActive(true);
            //luz.intensity = 6;
        }
    }
}
