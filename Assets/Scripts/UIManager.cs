using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region Singleton

    public static UIManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    #endregion

    Health playerHealth;

    [SerializeField] Image healthFill;

    private void OnDisable() {
        if (playerHealth != null)
            playerHealth.OnTakeDamage -= UpdateHealthBar;
    }

    public void SetPlayer(Health player) {
        playerHealth = player;
        playerHealth.OnTakeDamage += UpdateHealthBar;
    }

    void UpdateHealthBar() {
        healthFill.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}
