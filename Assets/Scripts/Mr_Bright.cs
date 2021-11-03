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
    private float DECELERATION = 6f;
    private float horizontalInput;
    private bool isRunning = false;
    public bool isFacingRight;
    private bool isChangingDirection => (rb.velocity.x > 0f && horizontalInput < 0f) || (rb.velocity.x < 0f && horizontalInput > 0f);
    public bool onRope = false;

    [Header("Jump")]
    private float JUMPFORCE = 5f;
    private float AIR_DECELERATION = 2f;
    [SerializeField] private float groundRaycastlength;
    private bool onGround;
    private int extraJumps = 1;
    private int currentJumps;
    private bool canJump => Input.GetKeyDown(KeyCode.Space) && (onGround || currentJumps > 0);

    [Header("Components")]
    public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    //Invisibilidad
    Light2D MrBrightLight;
    float timerInvisibilidad=0;
    bool statusInvisibilidad=false;

    //Monster Interaction
    InteractionMonsters interaction;


    private void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        MrBrightLight = this.GetComponent<Light2D>();
        MrBrightLight = this.GetComponent<Light2D>();
        interaction = this.GetComponent<InteractionMonsters>();
    }

    private void Update()
    {
        ProcessInputs();
        if (canJump) Jump();
        if (timerInvisibilidad<0){
            MrBrightLight.intensity=1.5f;
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
        rb.AddForce(Vector2.up * (JUMPFORCE-(extraJumps-currentJumps)), ForceMode2D.Impulse);
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
        MrBrightLight.intensity=0.8f;
    }

    public void DisableMrBright()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            interaction.iteractionWithMonsters(statusInvisibilidad, this.gameObject, col.gameObject, rb);
        }
    }

}
