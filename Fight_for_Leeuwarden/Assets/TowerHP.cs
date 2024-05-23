using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerHP : MonoBehaviour
{
    public int maxHealth = 1000;
    private int currentHealth;
    public LayerMask whatIsGround, whatIsEnemy;
    public float damageCooldown = 1f;
    public Slider healthSlider; // Reference to the UI Slider

    private bool canTakeDamage = true;

    // States
    public float attackRange;
    public bool EnemyInAttackRange;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Initialize the health UI
    }

    private void Update()
    {
        // Check if an enemy is in attack range
        EnemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        // If an enemy is in range, take damage
        if (EnemyInAttackRange) TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage && currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log("Tower took damage. Current health: " + currentHealth); // Debug log message

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                UpdateHealthUI(); // Update the health UI
                canTakeDamage = false;
                Invoke("ResetDamageCooldown", damageCooldown);
            }
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth; // Update the slider value
            Debug.Log("Updated health slider. Current value: " + healthSlider.value); // Debug log message
        }
        else
        {
            Debug.LogWarning("Health slider is not assigned."); // Warning if the slider is not assigned
        }
    }

    private void ResetDamageCooldown()
    {
        canTakeDamage = true;
    }

    private void Die()
    {
        currentHealth = maxHealth;
        Debug.Log("Tower Died! Restarting the scene."); // Debug log message
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
