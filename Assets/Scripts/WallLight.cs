using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLight : MonoBehaviour
{
    public GameObject lightAssigned;
    public GameObject pointEdge;
    GameObject[] monsters;

    void Update(){
        if (pointEdge.activeSelf == false)
        {
            monsters = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i=0;i<monsters.Length;i++){
                if (monsters[i].transform.position.x < pointEdge.transform.position.x){
                    monsters[i].SetActive(false);
                }
            }
            
        }
    }
}
