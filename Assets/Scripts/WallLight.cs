using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLight : MonoBehaviour
{
    public GameObject lightAssigned;
    public GameObject pointEdge;
    private bool isOn = false;

    public void TurnOn()
    {
        if (isOn) return;
        isOn = true;
        lightAssigned.SetActive(true);
        pointEdge.SetActive(false);
        ScoreController.instance.AddScore(100);
    }

}
