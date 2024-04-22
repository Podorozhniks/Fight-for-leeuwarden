using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth= 100;
    private int currentHealth;
    public LayerMask whatIsGround, whatIsEnemy;
    public float damageCooldown = 1f;
    public Slider healthSlider;

    private bool canTakeDamage = true;
    

    
    public float attackRange;
    public bool  EnemyInAttackRange;

    private void Update()
    {
        
        
        EnemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (EnemyInAttackRange) TakeDamage(20);

    }
    private void Start()
    {
        currentHealth = maxHealth;
        

    }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage && currentHealth > 0)
        {
            currentHealth -= damage;
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
        
       
        healthSlider.value = (float)currentHealth / maxHealth;
        
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
