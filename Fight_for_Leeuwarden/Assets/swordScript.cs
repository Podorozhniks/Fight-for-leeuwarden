using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sword : MonoBehaviour
{
    
    public int damage = 50;

    
    private void OnTriggerEnter(Collider other)
    {
        
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            
            enemy.HitBySword(damage);
        }
    }
}

