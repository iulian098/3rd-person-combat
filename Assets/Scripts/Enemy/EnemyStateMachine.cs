using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ThirdPersonCombat.Enemy {

    public class EnemyStateMachine : StateMachine {
        [SerializeField] Animator animator;
        [SerializeField] CharacterController characterController;
        [SerializeField] ForceReceiver forceReceiver;
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Weapon weapon;
        [SerializeField] Health health;
        [SerializeField] Target target;
        [SerializeField] Ragdoll ragdoll;
        [SerializeField] float movementSpeed;
        [SerializeField] float detectionRange;
        [SerializeField] float attackRange;
        [SerializeField] Attack[] attacks;

        Health player;

        public Animator Animator => animator;
        public CharacterController CharacterController => characterController;
        public ForceReceiver ForceReceiver => forceReceiver;
        public NavMeshAgent Agent => agent;
        public Weapon Weapon => weapon;
        public Target Target => target;
        public Ragdoll Ragdoll => ragdoll;
        public float MovementSpeed => movementSpeed;
        public float DetectionRange => detectionRange;
        public float AttackRange => attackRange;
        public Attack[] Attacks => attacks;
        public Health Player => player;

        private void OnEnable() {
            health.OnTakeDamage += OnTakeDamageHandle;
        }

        private void OnDisable() {
            health.OnTakeDamage -= OnTakeDamageHandle;
        }

        private void Start() {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }

        void OnTakeDamageHandle() {
            SwitchState(new EnemyImpactState(this));
        }

        void OnDieHandle() {
            SwitchState(new EnemyDeadState(this));
        }
    }
}
