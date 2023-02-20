using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{

    [SerializeField] CinemachineTargetGroup cinemachineTargetGroup;

    List<Target> targets = new List<Target>();
    Target currentTarget;

    public List<Target> Targets => targets;
    public Target CurrentTarget => currentTarget;

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

        currentTarget = targets[0];
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
