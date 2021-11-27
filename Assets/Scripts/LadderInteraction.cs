using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour
{
    private float vertical;
    private float speed = 2.5f;
    private bool isladder;
    private bool isclimbing;

    [SerializeField] private Mr_Bright player;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isladder && Mathf.Abs(vertical) > 0f)
        {
            isclimbing = true;
            player.rb.velocity = new Vector2(0, vertical * speed);
        }
    }

    private void FixedUpdate()
    {
        if (isclimbing) {
            player.rb.gravityScale = 0f;
            player.rb.velocity = new Vector2(player.rb.velocity.x, vertical * speed);
        }
        else if(!isclimbing && !player.onRope)
        {
            player.rb.gravityScale = 0.7f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {  
            isladder = false;
            isclimbing = false;
        }
    }
}
