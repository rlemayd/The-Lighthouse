using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableBehaviour : MonoBehaviour
{
    [SerializeField]private ActivableObject activableObject;
    private BoxCollider2D fuseCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        fuseCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mr Bright"))
        {
            activableObject.Activate();
            spriteRenderer.color = Color.red;
        }
    }
}
