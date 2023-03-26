using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerDodgingState : PlayerBaseState {

        readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
        readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
        readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");

        float remainingDodgeTime;

        Vector3 dodgeDirection;
        Vector3 movement;

        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgeDirection) : base(stateMachine) {
            this.dodgeDirection = dodgeDirection;
        }

        public override void OnEnter() {
            remainingDodgeTime = stateMachine.DodgeDuration;

            stateMachine.Animator.SetFloat(DodgeForwardHash, dodgeDirection.y);
            stateMachine.Animator.SetFloat(DodgeRightHash, dodgeDirection.x);
            stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, 0.1f);

            stateMachine.Health.IsInvulnerable = true;
        }

        public override void OnTick(float deltaTime) {
            movement = Vector3.zero;
            movement += stateMachine.transform.right * dodgeDirection.x * stateMachine.DodgeLenght / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgeDirection.y * stateMachine.DodgeLenght / stateMachine.DodgeDuration;

            Move(movement, deltaTime);
            FaceTarget();

            remainingDodgeTime -= deltaTime;

            if(remainingDodgeTime <= 0f) {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
        }

        public override void OnExit() {
            stateMachine.Health.IsInvulnerable = false;
        }
    }

}
