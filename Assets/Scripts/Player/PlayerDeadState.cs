using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerDeadState : PlayerBaseState {

        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) {
        }

        public override void OnEnter() {
            stateMachine.Ragdoll.ToggleRagdoll(true);
            stateMachine.Weapon.enabled = false;
        }

        public override void OnTick(float deltaTime) {
            
        }

        public override void OnExit() {
            
        }
    }

}
