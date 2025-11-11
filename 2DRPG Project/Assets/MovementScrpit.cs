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
        if (Keyboard.current.aKey.isPressed)
            moveInput = -1;
        if (Keyboard.current.dKey.isPressed)
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
