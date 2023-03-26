using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerFreeLookState : PlayerBaseState {

        readonly int FreeLookBlendTreeHash = Animator.StringToHash("Movement");
        readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        const float CrossFadeDuration = 0.1f;

        Vector3 movementVector = new Vector3();
        float dampTime = 0.1f;
        bool shouldFade;

        public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine) {
            this.shouldFade = shouldFade;
        }

        public override void OnEnter() {
            stateMachine.InputReader.onTarget += OnTarget;
            stateMachine.InputReader.onJump += OnJump;

            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);

            if (shouldFade)
                stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
            else
                stateMachine.Animator.Play(FreeLookBlendTreeHash);
        }


        public override void OnTick(float deltaTime) {

            if (stateMachine.InputReader.IsAttacking) {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            movementVector = CalculateMovement();

            Move(movementVector * stateMachine.FreeLookMovementSpeed, deltaTime);

            stateMachine.Animator.SetFloat(FreeLookSpeedHash, movementVector.magnitude, dampTime, deltaTime);


            if (stateMachine.InputReader.MovementVector == Vector2.zero) return;

            FaceMovementDirection(deltaTime);

        }

        public override void OnExit() {
            stateMachine.InputReader.onTarget -= OnTarget;
            stateMachine.InputReader.onJump -= OnJump;
        }

        Vector3 CalculateMovement() {
            Vector3 forward = stateMachine.CameraTransform.forward;
            Vector3 right = stateMachine.CameraTransform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputReader.MovementVector.y +
                right * stateMachine.InputReader.MovementVector.x;
        }

        void FaceMovementDirection(float deltaTime) {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
                Quaternion.LookRotation(movementVector),
                stateMachine.RotationDamping * deltaTime);
        }

        private void OnTarget() {

            if (!stateMachine.Targeter.SelectTarget()) return;

            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

        private void OnJump() {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

    }
}
