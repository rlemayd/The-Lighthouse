using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLevelDownDoor : MonoBehaviour
{   
    public Mr_Bright mrBright;
    public Maria maria;

    void Start()
    {      
    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Mr Bright") && maria.transform.position.x >= 14.5f){
            
            GoDownAFloor();
        }
    }

    private void GoDownAFloor(){
        switch (this.name)
        {
            case "ThirdFloorMrBrightDoor":
                mrBright.transform.position = GameObject.Find("SecondFloorMrBrightStartDoor").transform.position;
                maria.transform.position = GameObject.Find("SecondFloorMariaStartDoor").transform.position;
                break;
            case "SecondFloorMrBrightEndDoor":
                mrBright.transform.position = GameObject.Find("FirstFloorMrBrightStartDoor").transform.position;
                maria.transform.position = GameObject.Find("FirstFloorMariaStartDoor").transform.position;
                break;
            case "FirstFloorMrBrightRightDoor":
                GameObject.Find("FirstFloorMariaStartDoor").SetActive(true);
                mrBright.DisableMrBright();
                break;
            default:
                break;
        }
    }
}
