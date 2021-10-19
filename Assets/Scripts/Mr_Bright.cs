using Codice.Client.GameUI.Checkin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Mr_Bright : MonoBehaviour
{
    [Header("Movement")]
    private const float ACCELERATION = 8f;
    private float MAX_SPEED = 1.5f;
    private float MAX_RUNNING_SPEED = 3f;
    private float DECELERATION = 13f;
    private float horizontalInput;
    private bool isRunning = false;
    private bool isFacingRight;
    private bool isChangingDirection => (rb.velocity.x > 0f && horizontalInput < 0f) || (rb.velocity.x < 0f && horizontalInput > 0f);

    [Header("Jump")]
    private float JUMPFORCE = 3f;
    private float AIR_DECELERATION = 2f;
    [SerializeField] private float groundRaycastlength;
    private bool onGround;
    private int extraJumps = 1;
    private int currentJumps;
    private bool canJump => Input.GetKeyDown(KeyCode.Space) && (onGround || currentJumps > 0);

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    //Invisibilidad
    Light2D light;
    float timerInvisibilidad=0;
    bool statusInvisibilidad=false;


    //Cuerda
    private bool holding = false;
    public Transform holdinginto;
    private GameObject letgo;
    public Rope Rope;
    private void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        light = this.GetComponent<Light2D>();
    }

    private void Update()
    {
        ProcessInputs();
        if (canJump) Jump();
        if (timerInvisibilidad<0){
            light.intensity=1.5f;
            statusInvisibilidad = false;
        }
        else{
            timerInvisibilidad-=Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        CheckGroundCollision();
        Move();
        ProcessState();
        CheckRopeInputs();
    }

    void ProcessInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKey(KeyCode.F))
        {
            if(statusInvisibilidad == false){
                Invisibilidad();
                statusInvisibilidad = true;
            }
            
        }
    }

    void Jump()
    {
        if (!onGround) currentJumps--;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * JUMPFORCE, ForceMode2D.Impulse);
    }

    void ProcessState()
    {
        if ((isFacingRight && horizontalInput < 0) || (!isFacingRight && horizontalInput > 0))
        {
            Flip();
        }
    }

    void Move()
    {
        rb.AddForce(new Vector2(horizontalInput , 0) * ACCELERATION);
        float currentMaximumSpeed = isRunning ? MAX_RUNNING_SPEED : MAX_SPEED;
        if (Mathf.Abs(rb.velocity.x) > currentMaximumSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * currentMaximumSpeed, rb.velocity.y);
        }
        if (onGround)
        {
            currentJumps = extraJumps;
            ApplyGroundDeceleration();
        }
        else
        {
            ApplyAirDeceleration();
        }
    }

    void ApplyGroundDeceleration()
    {
        if (Mathf.Abs(horizontalInput) < 0.6f || isChangingDirection)
        {
            rb.drag = DECELERATION;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    void ApplyAirDeceleration()
    {
        rb.drag = AIR_DECELERATION;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = -scaler.x;
        transform.localScale = scaler;
    }

    void CheckGroundCollision()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastlength, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastlength);
    }

    void OnTriggerStay2D(Collider2D col){
        
        if (col.gameObject.CompareTag("Light") )
        {
            if (Input.GetKey(KeyCode.E)){
                col.gameObject.GetComponent<WallLight>().lightAssigned.SetActive(true);
                col.gameObject.GetComponent<WallLight>().pointEdge.SetActive(false);
                
            } 
        }
    }

    void Invisibilidad(){
        
        timerInvisibilidad = 5;
        light.intensity=0.8f;
        
    }

    void OnTriggerEnter2D(Collider2D colli)
    {if (!holding)
        {
            if (colli.gameObject.tag == "Rope") 
            {
            if (holdinginto != colli.gameObject.transform.parent)
              { 
                if (letgo ==null || colli.gameObject.transform.parent != letgo)
                        HoldRope(colli.gameObject.GetComponent<Rigidbody2D>());
              }
                    
            }
        }
    }
    public void HoldRope(Rigidbody2D rope) {
        rope.GetComponent<Rope>().isHolding = true;
        holding = true;
        holdinginto = Rope.gameObject.transform.parent;
    }
    void LetGo()
    { holding = false; }
    void CheckRopeInputs()
    { if (Input.GetKey("x"))
        { LetGo(); 
        }

      
    }
}
