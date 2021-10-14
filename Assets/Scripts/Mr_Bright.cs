using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mr_Bright : MonoBehaviour
{
    [Header("Movement")]
    private float ACCELERATION;
    private float MAX_SPEED;
    private float MAX_RUNNING_SPEED;
    private float DECELERATION;
    private float horizontalInput;
    private bool isRunning = false;
    private bool isFacingRight;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalInput < 0f) || (rb.velocity.x < 0f && horizontalInput > 0f);

    [Header("Jump")]
    private float JUMPFORCE;
    [SerializeField] private float groundRaycastlength;
    private float AIR_DECELERATION;
    private bool onGround;
    private int extraJumps = 1;
    private int currentJumps;
    private bool canJump => Input.GetKeyDown(KeyCode.Space) && (onGround || currentJumps > 0);

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        ACCELERATION = 8f;
        MAX_SPEED = 1.5f;
        MAX_RUNNING_SPEED = 3f;
        DECELERATION = 13f;
        JUMPFORCE = 2f;
        AIR_DECELERATION = 2;
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInputs();
        if (canJump) Jump();
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
        if (Mathf.Abs(horizontalInput) < 0.6f || changingDirection)
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

}
