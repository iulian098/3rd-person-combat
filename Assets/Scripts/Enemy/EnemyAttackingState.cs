using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Enemy {

    public class EnemyAttackingState : EnemyBaseState {

        float previousFrameTime;
        bool forceApplied;
        Attack attack;


        public EnemyAttackingState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine) {
            attack = stateMachine.Attacks[attackIndex];
        }

        public override void OnEnter() {
            stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
            stateMachine.Weapon.SetDamage(attack.Damage, attack.Knockback);
        }

        public override void OnTick(float deltaTime) {
            Move(deltaTime);

            FaceTarget();

            float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

            if (normalizedTime >= previousFrameTime && normalizedTime < 1f) {

                TryComboAttack(normalizedTime);
            }
            else {
                if (stateMachine.Player != null)
                    stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                else
                    stateMachine.SwitchState(new EnemyIdleState(stateMachine));

            }

            previousFrameTime = normalizedTime;

        }

        public override void OnExit() { }

        private void TryComboAttack(float normalizedTime) {
            if (attack.ComboStateIndex == -1 || normalizedTime < attack.ComboAttackTime || !IsInAttackRange()) return;

            stateMachine.SwitchState(new EnemyAttackingState(stateMachine, attack.ComboStateIndex));
        }
    }

}
