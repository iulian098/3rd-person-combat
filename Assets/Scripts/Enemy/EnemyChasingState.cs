using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Enemy {

    public class EnemyChasingState : EnemyBaseState {
        readonly int MovementHash = Animator.StringToHash("Movement");
        readonly int SpeedHash = Animator.StringToHash("Speed");
        const float CrossFadeBuration = 0.1f;
        const float AnimatorDampTime = 0.1f;

        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeBuration);
        }

        public override void OnTick(float deltaTime) {

            if (!IsInChaseRange()) {
                //Transition to idle state
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                return;
            }else if (IsInAttackRange()) {
                stateMachine.SwitchState(new EnemyAttackingState(stateMachine, 0));
                return;
            }

            MoveToPlayer(deltaTime);

            FaceTarget();

            stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);

        }

        public override void OnExit() {
            stateMachine.Agent.ResetPath();
            stateMachine.Agent.velocity = Vector3.zero;
        }

        void MoveToPlayer(float deltaTime) {
            if (stateMachine.Agent.isOnNavMesh) {
                stateMachine.Agent.destination = stateMachine.Player.transform.position;

                Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
            }

            stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
        }

    }
}
