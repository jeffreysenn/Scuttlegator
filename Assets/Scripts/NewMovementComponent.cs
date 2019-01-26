using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementComponent : MonoBehaviour
{
    [SerializeField] float walkAccelaration = 4;
    [SerializeField] float maxWalkSpeed = 9;
    [SerializeField] float groundJumpSpeed = 8;
    [SerializeField] float airJumpSpeed = 10;
    [SerializeField] float wallJumpSpeed = 10;
    [SerializeField] Vector2 rightWallJumpDirection = new Vector2(-1,3);

    Rigidbody2D rgBody;
    Vector3 targetVelocity;
    [SerializeField] float groundCheckOvershoot = .3f;
    [SerializeField] float wallCheckOvershoot = .3f;
    public LayerMask layerMask;

    public SpriteRenderer gunSprite;

    private bool hasHitWall = false;
    private bool wallIsRight = false;

    ContactFilter2D contactFilter;
    RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    [SerializeField] int airJumpNum = 2;
    private int airJumpedNum = 0;

    Vector2 targetDeltaVelocity = Vector2.zero;


    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float preSpeedY;

    private bool isGrounded = false;

    private Vector3 oriScale;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        preSpeedY = rgBody.velocity.y;

        oriScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //if (rgBody.velocity.x > 0.1f) { spriteRenderer.flipX = false; }
        //else if (rgBody.velocity.x < -0.1f) { spriteRenderer.flipX = true; }

        if (rgBody.velocity.x > 0.1f) { transform.localScale = oriScale; }
        else if (rgBody.velocity.x < -0.1f) { transform.localScale = new Vector3(-oriScale.x, oriScale.y, oriScale.z); }

        isGrounded = IsGrounded();

        animator.SetBool("grounded", IsGrounded());

        bool isFalling = (rgBody.velocity.y < preSpeedY);
        preSpeedY = rgBody.velocity.y;
        animator.SetBool("isFalling", isFalling);

        animator.SetFloat("velocityX", Mathf.Abs(rgBody.velocity.x));

        if (!hasHitWall && IsHittingWall()) { hasHitWall = true; }

        ComputeDeltaVelocity();

    }

    private void ComputeDeltaVelocity()
    {
        targetDeltaVelocity = Vector2.zero;
        if (Mathf.Abs(rgBody.velocity.x) < maxWalkSpeed && !(!IsGrounded() && IsHittingWall()))
        {
            targetDeltaVelocity += (Vector2)transform.right * Input.GetAxis("Horizontal") * walkAccelaration;
        }

        if (Input.GetButtonDown("Jump"))
        {
            hasHitWall = false;
            if (IsGrounded() || hasHitWall)
            {
                airJumpedNum = 0;
                targetDeltaVelocity += (Vector2)transform.up * (groundJumpSpeed - rgBody.velocity.y);
            }
            else if (IsHittingWall())
            {
                if (wallIsRight)
                {
                    targetDeltaVelocity += (rightWallJumpDirection).normalized * (wallJumpSpeed);
                }
                else
                {
                    Vector2 leftWallJumpDirection = new Vector2(-rightWallJumpDirection.x, rightWallJumpDirection.y);
                    targetDeltaVelocity += (leftWallJumpDirection).normalized * (wallJumpSpeed);
                }
            }
            else
            {
                if (airJumpedNum < airJumpNum)
                {
                    targetDeltaVelocity += (Vector2)transform.up * (airJumpSpeed - rgBody.velocity.y);
                }
                airJumpedNum++;
            }

        }
    }

    void FixedUpdate()
    {
        Physics2DExtensions.AddForce(rgBody, targetDeltaVelocity, ForceMode.VelocityChange);

        //if(Mathf.Abs(rgBody.velocity.x) < maxWalkSpeed)
        //{
        //    Physics2DExtensions.AddForce(rgBody, transform.right * Input.GetAxis("Horizontal") * walkAccelaration, ForceMode.VelocityChange);
        //}   

        //if (Input.GetButtonDown("Jump"))
        //{
        //    hasHitWall = false;
        //    if (IsGrounded() || hasHitWall)
        //    {
        //        airJumpedNum = 0;
        //        Physics2DExtensions.AddForce(rgBody, transform.up * (groundJumpSpeed - rgBody.velocity.y), ForceMode.VelocityChange);
        //    }
        //    else if (IsHittingWall())
        //    {
        //        if (wallIsRight)
        //        {
        //            Physics2DExtensions.AddForce(rgBody, (rightWallJumpDirection).normalized * (wallJumpSpeed), ForceMode.VelocityChange);
        //        }
        //        else
        //        {
        //            Vector2 leftWallJumpDirection = new Vector2(-rightWallJumpDirection.x, rightWallJumpDirection.y);
        //            Physics2DExtensions.AddForce(rgBody, (leftWallJumpDirection).normalized * (wallJumpSpeed), ForceMode.VelocityChange);

        //        }
        //    }
        //    else
        //    {
        //        if(airJumpedNum < airJumpNum)
        //        {
        //            Physics2DExtensions.AddForce(rgBody, transform.up * (airJumpSpeed - rgBody.velocity.y), ForceMode.VelocityChange);
        //        }
        //        airJumpedNum++;
        //    }

        //}

    }

    bool IsGrounded()
    {
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        RaycastHit2D hit = Physics2D.Raycast(capsuleCollider.bounds.center, -Vector2.up, 10, layerMask);
        if(hit.collider == null) { return false; }
        else
        {
            float centreToButtom = capsuleCollider.bounds.size.y / 2;
            if (Vector3.Distance(hit.point, transform.position) < centreToButtom + groundCheckOvershoot)
            {
                return true;
            }
        }
        return false;
    }

    bool IsHittingWall()
    {
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        Collider2D hitCollider = Physics2D.OverlapBox(capsuleCollider.bounds.center, new Vector2(capsuleCollider.bounds.size.x + wallCheckOvershoot * 2, capsuleCollider.bounds.size.y/5), 0, layerMask);
        if(hitCollider != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.right, capsuleCollider.bounds.size.x/2 + wallCheckOvershoot, layerMask);
            if(hit.collider != null)
            {
                wallIsRight = true;
            }
            else
            {
                wallIsRight = false;
            }
            return true;
        }
        return false;
    }

}
