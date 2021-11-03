using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorInteraction : MonoBehaviour
{
    public GameObject wonText, nextLevelButton, scareBoard;
    public Mr_Bright mrBright;
    public Maria maria;

    void Start()
    {
        //Canvas UI for won condition
        wonText.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Mr Bright") && maria.transform.position.x >= transform.position.x)
        {
            ActivateWinCondition();
        }
    }

    public void ActivateWinCondition()
    {
        wonText.SetActive(true);
        mrBright.DisableMrBright();
        scareBoard.SetActive(false);
        nextLevelButton.SetActive(true);
    }
}
