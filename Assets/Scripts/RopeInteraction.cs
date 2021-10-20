using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeInteraction : MonoBehaviour
{
    private bool holding = false;
    private float HORIZONTAL_IMPULSE = 7f;
    private float VERTICAL_IMPULSE = 5f;

    [SerializeField] private Mr_Bright MrBright;
    [SerializeField] private Rigidbody2D player;

    private void FixedUpdate()
    {
        CheckRopeInputs();
        if (holding)
        {
            player.velocity = new Vector2(0, 0);
            player.gravityScale = 0f;
        }
        else
        {
            player.gravityScale = 0.7f;
        }
    }

    void OnTriggerEnter2D(Collider2D colli)
    {
        if (!holding && colli.gameObject.tag == "Rope")
        {
            HoldRope();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rope")
        {
            holding = false;
        }
    }

    public void HoldRope()
    {
        holding = true;
    }

    void LetGo()
    { 
        holding = false;
        player.AddForce(new Vector2((MrBright.isFacingRight ? HORIZONTAL_IMPULSE : -HORIZONTAL_IMPULSE), VERTICAL_IMPULSE), ForceMode2D.Impulse);
    }

    void CheckRopeInputs()
    {
        if (Input.GetKey("x") && holding)
        {
            LetGo();
        }
    }
}
