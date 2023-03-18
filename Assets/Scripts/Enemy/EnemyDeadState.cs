using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Enemy {

    public class EnemyDeadState : EnemyBaseState {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Ragdoll.ToggleRagdoll(true);
            stateMachine.Weapon.enabled = false;
            GameObject.Destroy(stateMachine.Weapon);
        }

        public override void OnTick(float deltaTime) {
            
        }

        public override void OnExit() {
            
        }
    }
}
