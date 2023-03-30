using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerHangingState : PlayerBaseState {

        readonly int HangingHash = Animator.StringToHash("Hanging");

        Vector3 ledgeForward;
        Vector3 closestPoint;

        public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine) {
            this.ledgeForward = ledgeForward;
            this.closestPoint = closestPoint;
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(HangingHash, 0.1f);

            stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);

            stateMachine.CharacterController.enabled = false;
            stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
            stateMachine.CharacterController.enabled = true;
        }

        public override void OnTick(float deltaTime) {
            if(stateMachine.InputReader.MovementVector.y > 0f) {
                stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
            }
            else if (stateMachine.InputReader.MovementVector.y < 0f) {
                stateMachine.CharacterController.Move(Vector3.zero);
                stateMachine.ForceReceiver.Reset();
                stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            }
        }

        public override void OnExit() {
            
        }

        
    }

}
