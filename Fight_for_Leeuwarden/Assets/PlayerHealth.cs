using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //had to change the integers from maxHealth and currentHealth to floats for the damage reduction to work
    public float maxHealth = 100;
    private float currentHealth;
    public LayerMask whatIsGround, whatIsEnemy;
    public float damageCooldown = 1f;
    public Slider healthSlider;

    private bool canTakeDamage = true;

    //for shield blocking damage
    public WeaponController WeaponController;
    public float BlockedDamageMultiplier = 1f;

    //States
    public float attackRange;
    public bool EnemyInAttackRange;

    private void Update()
    {
        //checking if the shield is up or down
        BlockedDamageMultiplier = WeaponController.BlockedDamageMultiplier;

        //EnemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        EnemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (EnemyInAttackRange) TakeDamage(10);

    }
    private void Start()
    {
        currentHealth = maxHealth;
        //UpdateHealthUI();

    }
    public void TakeDamage(float damage)
    {
        if (canTakeDamage && currentHealth > 0)
        {
            currentHealth -= damage * BlockedDamageMultiplier;
            Debug.Log("Player health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                UpdateHealthUI();
                canTakeDamage = false;
                Invoke("ResetDamageCooldown", damageCooldown);
            }
        }
    }
    private void UpdateHealthUI()
    {

        //Debug.Log("Health slider value: " + healthSlider.value);
        healthSlider.value = (float)currentHealth / maxHealth;
        //Debug.Log("Current Health: " + currentHealth + ", Max Health: " + maxHealth);
    }
    private void ResetDamageCooldown()
    {

        canTakeDamage = true;
    }


    private void Die()
    {
        currentHealth = maxHealth;
        // Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}

