using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class swordCheck : MonoBehaviour
{
    
    public int health = 100;
    public int damage = 20;
    
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
