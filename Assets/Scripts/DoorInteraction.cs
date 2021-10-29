using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    //
    public GameObject wonText;
    public Mr_Bright mrBright;

    void Start()
    {
        //Canvas UI for won condition
        wonText.SetActive(false);
    }

    public void ActivateWinCondition()
    {
        wonText.SetActive(true);
        mrBright.DisableMrBright();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mr Bright"))
        {
            ActivateWinCondition();
        }
    }
}
