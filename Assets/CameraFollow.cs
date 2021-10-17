using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform mrBright;
    public float cameraDistance = 100.0f;
    public float zoom = 5;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = mrBright.position.x;
        pos.y = mrBright.position.y;
        transform.position = pos;
    }

    
}
