using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public sealed class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Collider2D bodyCollider;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField, Min(0.01f)] private float groundCheckHeight = 0.08f;

    [Header("Movement")]
    [SerializeField, Min(0.0f)] private float moveSpeed = Constants.Player.MoveSpeed;
    [SerializeField, Min(0.0f)] private float groundAcceleration = Constants.Player.GroundAcceleration;
    [SerializeField, Min(0.0f)] private float groundDeceleration = Constants.Player.GroundDeceleration;
    [SerializeField, Min(0.0f)] private float groundTurnAcceleration = Constants.Player.GroundTurnAcceleration;
    [SerializeField, Min(0.0f)] private float airAcceleration = Constants.Player.AirAcceleration;

    [Header("Jump")]
    [SerializeField, Min(0.0f)] private float jumpForce = Constants.Player.JumpForce;

    private float moveInput;
    private bool jumpRequested;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        body ??= GetComponent<Rigidbody2D>();
        bodyCollider ??= GetComponent<Collider2D>();

        if (groundLayers.value == 0)
        {
            groundLayers = LayerMask.GetMask(Layers.Environment);
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = CheckGrounded();
        ApplyHorizontalMovement();
        TryJump();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = Mathf.Clamp(input.x, -1.0f, 1.0f);
    }

    public void RequestJump()
    {
        jumpRequested = true;
    }

    private void ApplyHorizontalMovement()
    {
        float targetSpeed = moveInput * moveSpeed;
        bool hasMoveInput = !Mathf.Approximately(moveInput, 0.0f);

        if (!IsGrounded && !hasMoveInput)
        {
            return;
        }

        Vector2 velocity = body.linearVelocity;
        float acceleration = GetHorizontalAcceleration(hasMoveInput, targetSpeed, velocity.x);
        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
        body.linearVelocity = velocity;
    }

    private float GetHorizontalAcceleration(bool hasMoveInput, float targetSpeed, float currentSpeed)
    {
        if (!IsGrounded)
        {
            return airAcceleration;
        }

        if (!hasMoveInput)
        {
            return groundDeceleration;
        }

        bool isTurning = !Mathf.Approximately(currentSpeed, 0.0f)
            && Mathf.Sign(currentSpeed) != Mathf.Sign(targetSpeed);

        return isTurning ? groundTurnAcceleration : groundAcceleration;
    }

    private void TryJump()
    {
        if (!jumpRequested)
        {
            return;
        }

        jumpRequested = false;

        if (!IsGrounded)
        {
            return;
        }

        Vector2 velocity = body.linearVelocity;
        velocity.y = jumpForce;
        body.linearVelocity = velocity;
        IsGrounded = false;
    }

    private bool CheckGrounded()
    {
        Bounds bounds = bodyCollider.bounds;
        Vector2 checkCenter = new(bounds.center.x, bounds.min.y);
        Vector2 checkSize = new(Mathf.Max(0.01f, bounds.size.x * 0.8f), groundCheckHeight);

        return Physics2D.OverlapBox(checkCenter, checkSize, 0.0f, groundLayers) != null;
    }
}
