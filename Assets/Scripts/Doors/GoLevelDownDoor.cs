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

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Mr Bright") && maria.transform.position.x >= transform.position.x)
        {
            GoDownAFloor();
        }
    }

    private void GoDownAFloor(){
        switch (this.name)
        {
            case "ThirdFloorMrBrightDoor":
                mrBright.transform.position = GameObject.Find("SecondFloorMrBrightStartDoor").transform.position;
                UpdateMaria(GameObject.Find("SecondFloorMariaStartDoor").transform.position);
                break;
            case "SecondFloorMrBrightEndDoor":
                mrBright.transform.position = GameObject.Find("FirstFloorMrBrightStartDoor").transform.position;
                UpdateMaria(GameObject.Find("FirstFloorMariaStartDoor").transform.position);
                break;
            case "FirstFloorMrBrightRightDoor":
                GameObject.Find("FirstFloorMariaStartDoor").SetActive(true);
                mrBright.DisableMrBright();
                break;
            default:
                break;
        }
    }

    private void UpdateMaria(Vector3 position)
    {
        maria.transform.position = position;
        maria.positionMaria = maria.transform.position;
        maria.SlowDown();
    }
}
