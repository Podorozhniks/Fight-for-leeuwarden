using System.Collections;
using System.Collections.Generic;
using UnityEngine;



<<<<<<< HEAD
public class Enemy : MonoBehaviour
{
    
    public int health = 100;

=======
public class swordCheck : MonoBehaviour
{
    
    public int health = 100;
    public int damage = 20;
>>>>>>> origin/Artem_Dorozhkin
    
    public void HitBySword(int damage)
    {
        
        health -= damage;

        
        if (health <= 0)
        {
            // Destroy the enemy object
            Destroy(gameObject);
        }
    }
<<<<<<< HEAD
=======


>>>>>>> origin/Artem_Dorozhkin
}
