using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float drag = 0.3f;

    float verticalVelocity;
    private Vector3 dampingVelocity;
    private Vector3 impact;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    void Update()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (agent == null) return;

        if (impact.sqrMagnitude < 0.2f * 0.2f && !agent.enabled) {
            impact = Vector3.zero;
            agent.enabled = true;
        }
    }

    public void AddForce(Vector3 force) {
        impact += force;

        if (agent != null)
            agent.enabled = false;
    }
}
