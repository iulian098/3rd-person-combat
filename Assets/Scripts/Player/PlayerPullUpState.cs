using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerPullUpState : PlayerBaseState {

        readonly int PullUpHash = Animator.StringToHash("PullUp");

        Vector3 Offset = new Vector3(0, 1.71f, 0.75f);

        public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, 0.1f);

        }

        public override void OnTick(float deltaTime) {

            if (GetNormalizedTime(stateMachine.Animator, "Climbing") < 1f) return;

            stateMachine.CharacterController.enabled = false;
            stateMachine.transform.Translate(Offset, Space.Self);
            stateMachine.CharacterController.enabled = true;

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        public override void OnExit() {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
        }
    }

}
