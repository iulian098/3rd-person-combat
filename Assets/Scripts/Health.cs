using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    float health;

    public Action OnTakeDamage;
    public Action OnDie;

    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int dmg) {
        if (health == 0) return;

        health = Mathf.Max(health - dmg, 0);

        if (health == 0) OnDie?.Invoke();

        OnTakeDamage?.Invoke();
    }

}
