using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    int totalLightsCount;
    int lightsOnCount;
    void Start()
    {
        lightsOnCount = 0;
        totalLightsCount = gameObject.GetComponentsInChildren<WallLight>().Length;
    }

    public void UpdateLightsCount()
    {
        lightsOnCount += 1;
        if(totalLightsCount == lightsOnCount)
        {
            Maria maria = GameObject.FindGameObjectWithTag("Maria").GetComponent<Maria>();
            maria.SpeedUp();
        }
    }
}
