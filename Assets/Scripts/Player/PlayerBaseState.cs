using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public abstract class PlayerBaseState : State {
        protected PlayerStateMachine stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }

        protected void Move(Vector3 motion, float deltaTime) {
            stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected void Move(float deltaTime) {
            Move(Vector3.zero, deltaTime);
        }

        protected void FaceTarget() {
            if (stateMachine.Targeter.CurrentTarget == null) return;

            Vector3 dir = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
            dir.y = 0;
            stateMachine.transform.rotation = Quaternion.LookRotation(dir);
        }

        protected void ReturnToLocomotion() {
            if (stateMachine.Targeter.CurrentTarget != null)
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            else
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

}
