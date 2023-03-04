using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    float health;

    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int dmg) {
        if (health == 0) return;

        health = Mathf.Max(health - dmg, 0);
    }

}
