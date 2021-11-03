using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope2Interaction : MonoBehaviour
{
    [SerializeField] private Mr_Bright mb;
    [SerializeField] private Rigidbody2D playerrb2s;
    private float HJump = 7f;
    private float VJump = 5f;
    private float thrust=1.0f;

    void Start()
    {
        mb.onRope = false;
    }

    void Update()
    {
        CheckRope2Inputs();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    if(collision.gameObject.tag == "Rope2" && !mb.onRope)
        {
            MovingAlong();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Rope2")
         {
            mb.onRope = false;

         }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Rope2" && mb.onRope)
        {
            gameObject.transform.parent = collision.gameObject.transform;
            mb.rb.gravityScale = 0f;
            mb.rb.velocity = Vector3.zero;
        }
    }

    void LetGo()
    {
        mb.onRope = false;
        playerrb2s.AddForce(new Vector2((mb.isFacingRight ? HJump : -HJump), VJump), ForceMode2D.Impulse);
        gameObject.transform.parent = null;
        gameObject.transform.eulerAngles = Vector3.zero;
    }

    void CheckRope2Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) && mb.onRope)
        {
            LetGo();
        }
    }

    void MovingAlong()
    {
        mb.onRope = true;
    }
}
