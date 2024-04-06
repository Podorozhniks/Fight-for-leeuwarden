using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    
    public int health = 100;

    
    public void HitBySword(int damage)
    {
        
        health -= damage;

        
        if (health <= 0)
        {
            // Destroy the enemy object
            Destroy(gameObject);
        }
    }
}
