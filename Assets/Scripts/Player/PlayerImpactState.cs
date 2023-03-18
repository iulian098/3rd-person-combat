using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerImpactState : PlayerBaseState {

        readonly int GetHitHash = Animator.StringToHash("GetHit");
        const float CrossFadeDuration = 0.1f;

        float duration = 1f;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(GetHitHash, CrossFadeDuration);
        }

        public override void OnTick(float deltaTime) {
            Move(deltaTime);

            duration -= deltaTime;

            if(duration <= 0) {
                ReturnToLocomotion();
            }
        }

        public override void OnExit() {
            
        }
    }

}
