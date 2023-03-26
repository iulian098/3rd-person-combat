using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Enemy {

    public abstract class EnemyBaseState : State {
        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }

        protected void Move(Vector3 motion, float deltaTime) {
            stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected void Move(float deltaTime) {
            Move(Vector3.zero, deltaTime);
        }


        protected bool IsInChaseRange() {
            if (stateMachine.Player.IsDead) return false;

            float playerDist = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDist <= stateMachine.DetectionRange * stateMachine.DetectionRange;
        }

        protected bool IsInAttackRange() {
            if (stateMachine.Player.IsDead) return false;

            float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
        }

        protected void FaceTarget() {
            if (stateMachine.Player == null) return;

            Vector3 dir = stateMachine.Player.transform.position - stateMachine.transform.position;
            dir.y = 0;
            stateMachine.transform.rotation = Quaternion.LookRotation(dir);
        }

    }
}
