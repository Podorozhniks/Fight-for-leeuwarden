using System.Collections;
using System.Collections.Generic;
using UnityEngine;



<<<<<<< HEAD
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
=======
public class swordScript : MonoBehaviour
{

    public WeaponController wc;
    //public GameObject HitParticle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wc.IsAttacking)
        {
            Debug.Log(other.name);
            Destroy(other.gameObject);

            //Instantiate(HitParticle, new Vector3(other.transform.position.x,
            //    transform.position.y,other.transform.position.z),
            //   other.transform.rotation);

        }
    }



>>>>>>> origin/Artem_Dorozhkin
}

