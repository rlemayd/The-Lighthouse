using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope2Interaction : MonoBehaviour
{
    private Mr_Bright player;
    private Rigidbody2D roperb2d;
    private Rigidbody2D playerrb2s;
 
    bool onRope;

    // Start is called before the first frame update
    void Start()
    {
        onRope=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
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
            roperb2d = null;
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
        onRope = false;
        
    }

    void CheckRope2Inputs()
    {
        if (Input.GetKey("x") && onRope)
        {
            LetGo();
        }
    }
    void MovingAlong()
    {
       
        onRope = true;


    }



}
