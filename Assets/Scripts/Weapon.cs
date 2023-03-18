using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Collider myColl;

    int damage;
    float knockback;

    HashSet<Collider> collidedWith = new HashSet<Collider>();

    public void OnEnable() {
        collidedWith.Clear();
    }

    public void SetDamage(int damage, float knockback) {
        this.damage = damage;
        this.knockback = knockback;
    }

    private void OnTriggerEnter(Collider other) {
        if (other == myColl || collidedWith.Contains(other)) return;

        collidedWith.Add(other);

        if(other.TryGetComponent(out Health health))
            health.DealDamage(damage);

        if(other.TryGetComponent(out ForceReceiver forceReceiver))
            forceReceiver.AddForce((other.transform.position - myColl.transform.position).normalized * knockback);
        
    }
}
