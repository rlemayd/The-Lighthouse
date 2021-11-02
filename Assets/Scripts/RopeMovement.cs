using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{

    Rigidbody2D rb2d;

    public float speed;
    public float left;
    public float right;
    bool ismovingleft;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public void Changedirection()
    {
        if (transform.rotation.z > right)
        {ismovingleft=false;}
        if (transform.rotation.z < left)
        {ismovingleft=true;}

    }
    public void Movement()
    {
        Changedirection();

        if(ismovingleft)
        {rb2d.angularVelocity=speed;}
        if (!ismovingleft)
        {rb2d.angularVelocity= -1*speed;}
    }

}
