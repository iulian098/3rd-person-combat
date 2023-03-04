using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] CapsuleCollider weaponCollider;

    public void ToggleWeapon(int val) {
        weaponCollider.enabled = val == 1;
    }
}
