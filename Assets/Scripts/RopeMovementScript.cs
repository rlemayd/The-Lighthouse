using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 60, 90)-45);
    }
}
