using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{

    [SerializeField] CinemachineTargetGroup cinemachineTargetGroup;

    HashSet<Target> targets = new HashSet<Target>();
    Target currentTarget;
    Camera cam;

    public HashSet<Target> Targets => targets;
    public Target CurrentTarget => currentTarget;

    private void Start() {
        cam = Camera.main;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy") && other.TryGetComponent(out Target target))
            AddTarget(target);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy") && other.TryGetComponent(out Target target))
            RemoveTarget(target);
    }

    public bool SelectTarget() {
        if (targets.Count == 0) return false;

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (var target in targets) {
            Vector2 viewPos = cam.WorldToViewportPoint(target.transform.position);

            if(viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) continue;

            Vector2 center = viewPos - new Vector2(0.5f, 0.5f);

            if (center.sqrMagnitude < closestTargetDistance) {
                closestTarget = target;
                closestTargetDistance = center.sqrMagnitude;
            }
        }

        if (closestTarget == null) return false;

        currentTarget = closestTarget;
        cinemachineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);

        return true;
    }

    public void CancelTargeting() {
        cinemachineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    public void AddTarget(Target t) {
        t.OnDestroyed += RemoveTarget;
        targets.Add(t);
    }

    public void RemoveTarget(Target t) {
        if (currentTarget == t) CancelTargeting();

        t.OnDestroyed -= RemoveTarget;
        targets.Remove(t);
    }
}
