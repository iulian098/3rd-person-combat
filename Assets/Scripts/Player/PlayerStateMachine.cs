using UnityEngine;

namespace ThirdPersonCombat.Player {

    public class PlayerStateMachine : StateMachine {
        [SerializeField] InputReader inputReader;
        [SerializeField] CharacterController characterController;
        [SerializeField] Animator animator;
        [SerializeField] ForceReceiver forceReceiver;
        [SerializeField] Targeter targeter;
        [SerializeField] Weapon weapon;
        [SerializeField] Health health;
        [SerializeField] Ragdoll ragdoll;
        [SerializeField] float freeLookMovementSpeed;
        [SerializeField] float targetingMovementSpeed;
        [SerializeField] float rotationDamping;
        [SerializeField] Attack[] attacks;
        Transform cameraTransform;

        public InputReader InputReader => inputReader;
        public CharacterController CharacterController => characterController;
        public Animator Animator => animator;
        public ForceReceiver ForceReceiver => forceReceiver;
        public Targeter Targeter => targeter;
        public Weapon Weapon => weapon;
        public Ragdoll Ragdoll => ragdoll;
        public float FreeLookMovementSpeed => freeLookMovementSpeed;
        public float TargetingMovementSpeed => targetingMovementSpeed;
        public float RotationDamping => rotationDamping;
        public Attack[] Attacks => attacks;
        public Transform CameraTransform => cameraTransform;

        private void OnEnable() {
            health.OnTakeDamage += OnTakeDamageHandle;
            health.OnDie += OnDieHandle;
        }

        private void OnDisable() {
            health.OnTakeDamage -= OnTakeDamageHandle;
            health.OnDie -= OnDieHandle;
        }

        private void Start() {
            cameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreeLookState(this));
        }

        private void OnTakeDamageHandle() {
            SwitchState(new PlayerImpactState(this));
        }

        private void OnDieHandle() {
            SwitchState(new PlayerDeadState(this));
        }
        
    }
}
