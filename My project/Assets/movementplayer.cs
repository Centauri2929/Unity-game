using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementplayer : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    float dirX , dirY;
    Rigidbody2D rb;
    public float groundHeight = 10;
    public float gravity;
    public float jumpVelocity = 20;
    public bool isGrounded = false;
    public Vector2 velocity;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxJumpTimer = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }


    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true; 
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
        dirX = Input.GetAxis ("Horizontal") * moveSpeed;
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            pos.y += gravity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (pos.y <=groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
        
        rb.velocity = new Vector2 (dirX , dirY);
    }
}
