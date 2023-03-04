using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Collider myColl;

    int damage;

    HashSet<Collider> collidedWith = new HashSet<Collider>();

    private void OnEnable() {
        collidedWith.Clear();
    }

    public void SetDamage(int dmg) {
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other) {
        if (other == myColl || collidedWith.Contains(other)) return;

        collidedWith.Add(other);

        if(other.TryGetComponent(out Health health))
            health.DealDamage(damage);

        Debug.Log($"{name} health {health}");
    }
}
