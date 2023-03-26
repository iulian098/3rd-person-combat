using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerHangingState : PlayerBaseState {

        readonly int HangingHash = Animator.StringToHash("Hanging");

        Vector3 ledgeForward;

        public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward) : base(stateMachine) {
            this.ledgeForward = ledgeForward;
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(HangingHash, 0.1f);

            stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
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
