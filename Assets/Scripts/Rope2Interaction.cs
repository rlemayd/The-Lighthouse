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
 
    bool onRope;

    // Start is called before the first frame update


    void Start()
    {
        onRope=false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRope2Inputs();

    }
    private void FixedUpdate()
    {
    
    }


    

    void OnTriggerEnter2D(Collider2D collision)
    {
    if(collision.gameObject.tag == "Rope2" && !onRope)
        {
            MovingAlong();
            
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Rope2")
         {
            onRope = false;

         }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Rope2")
        {
            Vector3 positionrope=collision.gameObject.transform.position;
            positionrope.z=0f;
            transform.position=positionrope;
            

        }



    }
    void LetGo()
    { 
        Debug.Log("fsfsf");
        onRope = false;
 
        playerrb2s.AddForce(new Vector2((mb.isFacingRight ? HJump : -HJump), VJump), ForceMode2D.Impulse);
    }

    void CheckRope2Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onRope)
        {
        LetGo();
        }
    }
    void MovingAlong()
    {
       
        onRope = true;


    }

 


}
