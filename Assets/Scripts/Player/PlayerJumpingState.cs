using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerJumpingState : PlayerBaseState {

        readonly int JumpHash = Animator.StringToHash("Jump");
        private Vector3 momentum;

        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

            momentum = stateMachine.CharacterController.velocity;
            momentum.y = 0;

            stateMachine.Animator.CrossFadeInFixedTime(JumpHash, 0.1f);

            stateMachine.LedgeDetector.OnLedgeDetected += OnLedgeDetected;
        }

        public override void OnTick(float deltaTime) {
            Move(momentum, deltaTime);

            if(stateMachine.CharacterController.velocity.y <= 0) {
                //Switch to falling state
                stateMachine.SwitchState(new PlayerFallingState(stateMachine));
                return;
            }

            FaceTarget();
        }

        public override void OnExit() {
            stateMachine.LedgeDetector.OnLedgeDetected -= OnLedgeDetected;
        }

        private void OnLedgeDetected(Vector3 ledgeForward, Vector3 closestPoint) {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, closestPoint));
        }

    }

}
