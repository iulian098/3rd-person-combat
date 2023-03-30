using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerFallingState : PlayerBaseState {
        readonly int FallHash = Animator.StringToHash("Fall");

        Vector3 momentum;

        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            momentum = stateMachine.CharacterController.velocity;
            momentum.y = 0;
            stateMachine.Animator.CrossFadeInFixedTime(FallHash, 0.1f);

            stateMachine.LedgeDetector.OnLedgeDetected += OnLedgeDetected;
        }

        public override void OnTick(float deltaTime) {
            Move(momentum, deltaTime);

            if (stateMachine.CharacterController.isGrounded) {
                ReturnToLocomotion();
                return;
            }

            FaceTarget();
        }

        public override void OnExit() {
            stateMachine.LedgeDetector.OnLedgeDetected -= OnLedgeDetected;
        }

        void OnLedgeDetected(Vector3 ledgeForward, Vector3 closestPoint) {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, closestPoint));
        }
    }
}
