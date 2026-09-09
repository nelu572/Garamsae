using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerMovement movement;

    public Vector2 MoveInput { get; private set; }

    private void Awake()
    {
        inputHandler ??= GetComponent<PlayerInputHandler>();
        movement ??= GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (inputHandler == null)
        {
            return;
        }

        inputHandler.MoveChanged += HandleMoveChanged;
        inputHandler.JumpRequested += HandleJumpRequested;
        MoveInput = inputHandler.MoveInput;
        movement?.SetMoveInput(MoveInput);
    }

    private void OnDisable()
    {
        if (inputHandler == null)
        {
            return;
        }

        inputHandler.MoveChanged -= HandleMoveChanged;
        inputHandler.JumpRequested -= HandleJumpRequested;
    }

    private void HandleMoveChanged(Vector2 moveInput)
    {
        MoveInput = moveInput;
        movement?.SetMoveInput(MoveInput);
    }

    private void HandleJumpRequested()
    {
        movement?.RequestJump();
    }
}
