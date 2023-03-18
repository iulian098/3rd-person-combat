using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Enemy {

    public class EnemyIdleState : EnemyBaseState {

        readonly int MovementHash = Animator.StringToHash("Movement");
        readonly int SpeedHash = Animator.StringToHash("Speed");
        const float CrossFadeBuration = 0.1f;
        const float AnimatorDampTime = 0.1f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeBuration);
        }

        public override void OnTick(float deltaTime) {
            Move(deltaTime);
            if (IsInChaseRange()) {
                //Transition to chasing state
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }

            stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltaTime);

        }

        public override void OnExit() {
        }
    }
}
