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

    private bool hasHitWall = false;
    private bool wallIsRight = false;

    ContactFilter2D contactFilter;
    RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    [SerializeField] int airJumpNum = 2;
    private int airJumpedNum = 0;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rgBody.velocity.x > 0) { spriteRenderer.flipX = false; }
        else if (rgBody.velocity.x < 0) { spriteRenderer.flipX = true; }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        if(!hasHitWall && IsHittingWall()) { hasHitWall = true; }
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(rgBody.velocity.x) < maxWalkSpeed)
        {
            Physics2DExtensions.AddForce(rgBody, transform.right * Input.GetAxis("Horizontal") * walkAccelaration, ForceMode.VelocityChange);
        }   

        if (Input.GetButtonDown("Jump"))
        {
            hasHitWall = false;
            if (IsGrounded() || hasHitWall)
            {
                airJumpedNum = 0;
                Physics2DExtensions.AddForce(rgBody, transform.up * (groundJumpSpeed - rgBody.velocity.y), ForceMode.VelocityChange);
            }
            else if (IsHittingWall())
            {
                if (wallIsRight)
                {
                    Physics2DExtensions.AddForce(rgBody, (rightWallJumpDirection).normalized * (wallJumpSpeed), ForceMode.VelocityChange);
                }
                else
                {
                    Vector2 leftWallJumpDirection = new Vector2(-rightWallJumpDirection.x, rightWallJumpDirection.y);
                    Physics2DExtensions.AddForce(rgBody, (leftWallJumpDirection).normalized * (wallJumpSpeed), ForceMode.VelocityChange);

                }
            }
            else
            {
                if(airJumpedNum < airJumpNum)
                {
                    Physics2DExtensions.AddForce(rgBody, transform.up * (airJumpSpeed - rgBody.velocity.y), ForceMode.VelocityChange);
                }
                airJumpedNum++;
            }

        }

    }

    bool IsGrounded()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, -Vector2.up, 10, layerMask);
        if(hit.collider == null) { return false; }
        else
        {
            float centreToButtom = boxCollider.bounds.size.y / 2;
            if (Vector3.Distance(hit.point, transform.position) < centreToButtom + groundCheckOvershoot)
            {
                return true;
            }
        }
        return false;
    }

    bool IsHittingWall()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Collider2D hitCollider = Physics2D.OverlapBox(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x + wallCheckOvershoot * 2, boxCollider.bounds.size.y), 0, layerMask);
        if(hitCollider != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, 10, layerMask);
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
