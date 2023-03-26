using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerTargetingState : PlayerBaseState {

        readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
        readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

        const float CrossFadeDuration = 0.1f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            Debug.Log("Entering Targeting State");

            stateMachine.InputReader.onTarget += OnCancelTarget;
            stateMachine.InputReader.onDodge += OnDodge;
            stateMachine.InputReader.onJump += OnJump;

            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
        }

        public override void OnTick(float deltaTime) {
            if (stateMachine.InputReader.IsAttacking) {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            if (stateMachine.InputReader.IsBlocking) {
                stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
                return;
            }

            if (stateMachine.Targeter.CurrentTarget == null) {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }

            Vector3 movement = CalculateMovement(deltaTime);

            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

            UpdateAnim(deltaTime);

            FaceTarget();
        }


        public override void OnExit() {
            stateMachine.InputReader.onTarget -= OnCancelTarget;
            stateMachine.InputReader.onDodge -= OnDodge;
            stateMachine.InputReader.onJump -= OnJump;

        }

        private void OnCancelTarget() {
            stateMachine.Targeter.CancelTargeting();
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        private void OnDodge() {
            if (stateMachine.InputReader.MovementVector == Vector2.zero)
                stateMachine.SwitchState(new PlayerDodgingState(stateMachine, Vector2.down));
            else
                stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementVector));
        }

        private void OnJump() {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private Vector3 CalculateMovement(float deltaTime) {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.InputReader.MovementVector.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementVector.y;

            return movement;
        }

        private void UpdateAnim(float deltaTime) {

            stateMachine.Animator.SetFloat(TargetingRightHash, stateMachine.InputReader.MovementVector.x == 0 ? 0 : stateMachine.InputReader.MovementVector.x, 0.1f, deltaTime);
            stateMachine.Animator.SetFloat(TargetingForwardHash, stateMachine.InputReader.MovementVector.y == 0 ? 0 : stateMachine.InputReader.MovementVector.y, 0.1f, deltaTime);
        }


    }
}
