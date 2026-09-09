using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public event Action<Vector2> MoveChanged;
    public event Action JumpRequested;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        MoveChanged?.Invoke(MoveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpRequested?.Invoke();
        }
    }
}
