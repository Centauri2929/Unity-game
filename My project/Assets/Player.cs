using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxHoldJumpTimer = 0.0f;

    public float jumpGroundThreshold = 1;

    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;
    

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded|| groundDistance <= jumpGroundThreshold)
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
        
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {   
        velocity = new Vector2(horizontal * speed,velocity.y);

        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                maxHoldJumpTimer += Time.fixedDeltaTime;
                if (maxHoldJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }


            pos.y += velocity.y * Time.fixedDeltaTime;

            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded= true;
                maxHoldJumpTimer = 0;
            }
        }

        transform.position = pos;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal <0f || !isFacingRight && horizontal >0f)
        {
            isFacingRight = !isFacingRight;
        }
    }
    
}
