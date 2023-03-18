using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Enemy {

    public class EnemyImpactState : EnemyBaseState {
        readonly int GetHitHash = Animator.StringToHash("GetHit");
        const float CrossFadeDuration = 0.1f;

        float duration = 1f;



        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(GetHitHash, CrossFadeDuration);
        }

        public override void OnTick(float deltaTime) {
            Move(deltaTime);

            duration -= deltaTime;

            if (duration <= 0) {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
        }

        public override void OnExit() {
        }
    }
}
