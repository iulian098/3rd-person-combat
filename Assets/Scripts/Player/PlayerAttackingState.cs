using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerAttackingState : PlayerBaseState {

        float previousFrameTime;
        bool forceApplied;
        Attack attack;

        public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) {
            attack = stateMachine.Attacks[attackIndex];
        }

        public override void OnEnter() {
            stateMachine.Weapon.SetDamage(attack.Damage, attack.Knockback);
            stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        }

        public override void OnTick(float deltaTime) {
            Move(deltaTime);

            FaceTarget();

            float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

            if (normalizedTime >= previousFrameTime && normalizedTime < 1f) {
                TryApplyForce(normalizedTime);

                if (stateMachine.InputReader.IsAttacking)
                    TryComboAttack(normalizedTime);
            }
            else {
                if (stateMachine.Targeter.CurrentTarget != null)
                    stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                else
                    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));

            }

            previousFrameTime = normalizedTime;
        }

        public override void OnExit() { }

        private void TryComboAttack(float normalizedTime) {
            if (attack.ComboStateIndex == -1 || normalizedTime < attack.ComboAttackTime) return;

            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
        }

        void TryApplyForce(float normalizedTime) {
            if (normalizedTime < attack.ForceTime || forceApplied) return;

            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

            forceApplied = true;
        }
    }

}
