using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 velocity;
    public Rigidbody2D rb;

    [Header("GRAVITY")]
    public float gravityAcc;
    public float groundCheckDistance;
    public bool grounded;
    [Header("VISUALS")]
    public bool facingRight;
    public float runAnimationThreshold;
    public SpriteRenderer spriteRenderer;
    [Header("RUNNING")]
    public float runAcc;
    [Range(0, 1)]
    public float groundDrag;
    [Range(0, 1)]
    public float airDrag;
    public Animator animator;
    public Collider2D collider;

    public bool debug;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        ApplyGravity();
        InputHandling();
        if(grounded)
        {
            velocity.x *= groundDrag;
        }
        else
        {
            velocity.x *= airDrag;
        }

        animator.SetBool("Run", velocity.x > runAnimationThreshold || velocity.x < -runAnimationThreshold);

    }

    private void FixedUpdate()
    { 
        GroundCheck();

        rb.MovePosition((Vector2)transform.position + velocity);
    }

    void InputHandling()
    {
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool rightDown = (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyDown(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow));
        bool leftDown = (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A)) || (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow));

        if(right && !left)
        {
            velocity.x += runAcc * Time.deltaTime;
            spriteRenderer.flipX = false;
        }
        if(left && !right)
        {
            velocity.x -= runAcc * Time.deltaTime;
            spriteRenderer.flipX = true;
        }
    }
    void ApplyGravity()
    {
        if (!grounded)
        {
            velocity.y -= gravityAcc * Time.deltaTime;
        }
        else if(velocity.y <0)
        {
            velocity.y = 0;
        }
    }
    void GroundCheck()
    {
        if (debug)
        {
            Debug.DrawRay(transform.position - new Vector3(0.8110075f / 2f, 0.8044906f / 2f), Vector3.down);
            Debug.DrawRay(transform.position - new Vector3(-0.8110075f / 2f, 0.8044906f / 2f), Vector3.down);
        }

        RaycastHit2D left = Physics2D.Raycast((Vector2)transform.position - new Vector2(0.8110075f / 2f, 0.8044906f / 2f), Vector2.down, groundCheckDistance, LayerMask.GetMask("Stage"));
        RaycastHit2D right = Physics2D.Raycast((Vector2)transform.position - new Vector2(-0.8110075f / 2f, 0.8044906f / 2f), Vector2.down, groundCheckDistance, LayerMask.GetMask("Stage"));
        Debug.Log(left.collider);
        grounded = (left.collider != null) || (right.collider != null);
    }
}
