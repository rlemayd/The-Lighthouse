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
    private bool isChangingDirection => (rb.velocity.x > 0f && horizontalInput < 0f) || (rb.velocity.x < 0f && horizontalInput > 0f);
    public bool isFacingRight;
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
    [SerializeField] private Animator animator;

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
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("verticalSpeed", rb.velocity.y);
        ProcessInputs();
        if (canJump) Jump();
        if (timerInvisibilidad<0){
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"),false);
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
        if (onRope) return;
        if (!onGround) currentJumps--;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (JUMPFORCE - (currentJumps < extraJumps-1 ? 1.5f : 0)), ForceMode2D.Impulse);
        animator.SetBool("isJumping", true);
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
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastlength, groundLayer);
        if (animator.GetBool("isJumping") && onGround && !wasOnGround) animator.SetBool("isJumping", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastlength);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Light") )
        {
            WallLight light = col.gameObject.GetComponent<WallLight>();
            if (Input.GetKey(KeyCode.E))
            {
                light.TurnOn();
            } 
        }
    }

    void Invisibilidad()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
        timerInvisibilidad = 5;
        MrBrightLight.intensity=0.8f;
    }

    public void DisableMrBright()
    {
        gameObject.SetActive(false);
    }

    public void InfiniteJumps()
    {
        extraJumps = int.MaxValue;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            interaction.iteractionWithMonsters(statusInvisibilidad, this.gameObject, col.gameObject, rb);
            ScoreController.instance.AddScore(-10);
        }
    }

}
