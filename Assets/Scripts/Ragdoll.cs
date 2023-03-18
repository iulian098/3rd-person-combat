using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;

    Collider[] colliders;
    Rigidbody[] rigidbodies;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        rigidbodies = GetComponentsInChildren<Rigidbody>(true);
    }

    public void ToggleRagdoll(bool isEnabled) {
        foreach (var coll in colliders) {
            if (coll.CompareTag("Ragdoll"))
                coll.enabled = isEnabled;
        }

        foreach (var rb in rigidbodies) {
            if (rb.CompareTag("Ragdoll")) {
                rb.isKinematic = !isEnabled;
                rb.useGravity = isEnabled;
            }
        }

        characterController.enabled = !isEnabled;
        animator.enabled = !isEnabled;
    }

}
