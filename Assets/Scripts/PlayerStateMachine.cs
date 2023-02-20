using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] InputReader inputReader;
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;
    [SerializeField] ForceReceiver forceReceiver;
    [SerializeField] Targeter targeter;
    [SerializeField] float freeLookMovementSpeed;
    [SerializeField] float targetingMovementSpeed;
    [SerializeField] float rotationDamping;
    Transform cameraTransform;

    public InputReader InputReader => inputReader;
    public CharacterController CharacterController => characterController;
    public Animator Animator => animator;
    public ForceReceiver ForceReceiver => forceReceiver;
    public Targeter Targeter => targeter;
    public float FreeLookMovementSpeed => freeLookMovementSpeed;
    public float TargetingMovementSpeed => targetingMovementSpeed;
    public float RotationDamping => rotationDamping;
    public Transform CameraTransform => cameraTransform;

    private void Start() {
        cameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
