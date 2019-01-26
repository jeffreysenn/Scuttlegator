using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 10;
    public float airJumpSpeed = 10;
    public int JumpNum = 3;

    private int jumpedNum = 0;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector3 prePos;
    public Vector3 vel;
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        prePos = transform.position;
    }

    protected override void ComputeVelocity()
    {
        vel = GetVelocity();
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (grounded)
        {
            jumpedNum = 0;
        }

        if(Input.GetButtonDown("Jump"))
        {
            velocity.y  += jumpTakeOffSpeed;
            //if(jumpedNum < JumpNum)
            //{
            //    if(jumpedNum == 0)
            //    {
            //        velocity.y = GetVelocity().y + jumpTakeOffSpeed;
            //    }
            //    else
            //    {
            //        velocity.y += GetVelocity().y + airJumpSpeed;
            //    }
            //    jumpedNum++;
            //}
        }
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    if (velocity.y > 0)
        //    {
        //        velocity.y = rb2d.velocity.y + velocity.y * 0.5f;
        //    }
        //}

        if (move.x > 0) {  spriteRenderer.flipX = false; }
        else if (move.x < 0) { spriteRenderer.flipX = true; }


        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    Vector3 GetVelocity()
    {
        Vector2 thisVelocity = (transform.position - prePos) / Time.deltaTime;
        prePos = transform.position;
        return thisVelocity;
    }
}