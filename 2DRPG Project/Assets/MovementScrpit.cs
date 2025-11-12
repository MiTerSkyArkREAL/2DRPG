using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float defaultMoveSpeed;
    public float sprintSpeed;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform rightWallCheck1;
    public Transform rightWallCheck2;
    public Transform rightWallCheck3;
    public Transform leftWallCheck1;
    public Transform leftWallCheck2;
    public Transform leftWallCheck3;
    public float wallCheckDistance = 0.1f;
    private bool isTouchingRightWall1;
    private bool isTouchingRightWall2;
    private bool isTouchingRightWall3;
    private bool isTouchingLeftWall1;
    private bool isTouchingLeftWall2;
    private bool isTouchingLeftWall3;
    private bool rightwallTouch;
    private bool leftWallTouch;


    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Assign movement speeds properly
        defaultMoveSpeed = moveSpeed;
        sprintSpeed = moveSpeed * 1.5f;
    }

    void Update()
    {
        moveInput = 0;

        // Left/right movement
        if (Keyboard.current.aKey.isPressed && !leftWallTouch)
            moveInput = -1;
        if (Keyboard.current.dKey.isPressed && !rightwallTouch)
            moveInput = 1;

        // Jump with space or W
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && isGrounded)
            Jump();

        // Sprint (Shift key)
        if (Keyboard.current.shiftKey.isPressed)
            moveSpeed = sprintSpeed;
        else
            moveSpeed = defaultMoveSpeed;
    }

    void FixedUpdate()
    {
        // Apply horizontal velocity
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingRightWall1 = Physics2D.OverlapCircle(rightWallCheck1.position, wallCheckDistance, groundLayer);
        isTouchingRightWall2 = Physics2D.OverlapCircle(rightWallCheck2.position, wallCheckDistance, groundLayer);
        isTouchingRightWall3 = Physics2D.OverlapCircle(rightWallCheck3.position, wallCheckDistance, groundLayer);
        isTouchingLeftWall1 = Physics2D.OverlapCircle(leftWallCheck1.position, wallCheckDistance, groundLayer);
        isTouchingLeftWall2 = Physics2D.OverlapCircle(leftWallCheck2.position, wallCheckDistance, groundLayer);
        isTouchingLeftWall3 = Physics2D.OverlapCircle(leftWallCheck3.position, wallCheckDistance, groundLayer);

        if(isTouchingRightWall1 || isTouchingRightWall2 || isTouchingRightWall3){
            rightwallTouch = true;
        }else{
            rightwallTouch = false;
        }
        if(isTouchingLeftWall1 || isTouchingLeftWall2 || isTouchingLeftWall3){
            leftWallTouch = true;
        }else{
            leftWallTouch = false;
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
