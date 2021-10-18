using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour
{
    private float vertical;
    private float speed = 2.5f;
    private bool isladder;
    private bool isclimbing;

    [SerializeField] private Rigidbody2D player;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isladder && Mathf.Abs(vertical) > 0f)
        {
            isclimbing = true;
            player.velocity = new Vector2(0, vertical * speed);
        }
    }

    private void FixedUpdate()
    {
        if (isclimbing) {
            player.gravityScale = 0f;
            player.velocity = new Vector2(player.velocity.x, vertical * speed);
        }
        else
        {
            player.gravityScale = 0.7f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")){
            isladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")){
            isladder = false;
            isclimbing = false;
        }
    }
}
