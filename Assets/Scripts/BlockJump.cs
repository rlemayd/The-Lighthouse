using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BlockJump : MonoBehaviour
{
    [SerializeField] public List<ActivableObject> activableObjects;

    private void Start()
    {
        PrepareObjects();
    }

    private void PrepareObjects()
    {
        foreach (ActivableObject activable in activableObjects)
        {
            activable.Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mr Bright"))
        {
            foreach (ActivableObject activableObject in activableObjects)
            {
                if (activableObject.IsActive())
                    activableObject.Deactivate();
                else
                    activableObject.Activate();
            }
        }
    }
}
