using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    float health;
    bool isInvulnerable;

    public Action OnTakeDamage;
    public Action OnDie;

    public bool IsDead => health == 0;
    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }

    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int dmg) {
        if (health == 0 || isInvulnerable) return;

        health = Mathf.Max(health - dmg, 0);

        if (health == 0) OnDie?.Invoke();

        OnTakeDamage?.Invoke();
    }

}
