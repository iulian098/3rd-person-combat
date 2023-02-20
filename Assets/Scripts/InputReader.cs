using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerControls.IPlayerActions
{
    public PlayerControls playerControls;

    public Vector2 MovementVector { get; private set; }

    public Action onJump;
    public Action onAttack;
    public Action onTarget;

    private void Start() {
        playerControls = new PlayerControls();
        playerControls.Player.SetCallbacks(this);

        playerControls.Player.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (!context.performed) return;

        onAttack?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (!context.performed) return;

        onJump?.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context) {
        MovementVector = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context) {
        if (!context.performed) return;

        onTarget?.Invoke();
    }
}
