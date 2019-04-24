using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 velocity;
    public Rigidbody2D rb;

    public float gravityAcc;
    public float groundCheckDistance;
    public bool grounded;

    public Animator animator;
    public Collider2D collider;

    public bool debug;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        ApplyGravity();
    }

    private void FixedUpdate()
    { 
        GroundCheck();

        rb.MovePosition((Vector2)transform.position + velocity);
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
            Debug.DrawRay(transform.position - new Vector3(0.8110075f / 2f, 0.75f / 2f), Vector3.down);
            Debug.DrawRay(transform.position - new Vector3(-0.8110075f / 2f, 0.75f / 2f), Vector3.down);
        }

        RaycastHit2D left = Physics2D.Raycast((Vector2)transform.position - new Vector2(0.8110075f / 2f, .75f / 2f), Vector2.down, groundCheckDistance, LayerMask.GetMask("Stage"));
        RaycastHit2D right = Physics2D.Raycast((Vector2)transform.position - new Vector2(-0.8110075f / 2f, .75f / 2f), Vector2.down, groundCheckDistance, LayerMask.GetMask("Stage"));
        Debug.Log(left.collider);
        grounded = (left.collider != null) || (right.collider != null);
    }
}
