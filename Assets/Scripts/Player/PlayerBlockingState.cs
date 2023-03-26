using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerBlockingState : PlayerBaseState {
        readonly int BlockingHash = Animator.StringToHash("Block");

        public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Health.IsInvulnerable = true;
            stateMachine.Animator.CrossFadeInFixedTime(BlockingHash, 0.1f);
        }

        public override void OnTick(float deltaTime) {
            Move(deltaTime);

            if (!stateMachine.InputReader.IsBlocking) {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                return;
            }

            if(stateMachine.Targeter.CurrentTarget == null) {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
        }

        public override void OnExit() {
            stateMachine.Health.IsInvulnerable = false;
        }
    }

}
