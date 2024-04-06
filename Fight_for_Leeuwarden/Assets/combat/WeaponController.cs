using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public stamina stamina;
    public float Stamina = 50;
    public bool CanAttack = true;
    public float AttackCooldown = 0.01f;
    public bool IsAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        Stamina = stamina.Stamina;
    }

    // Update is called once per frame
    void Update()
    {
        Stamina = stamina.Stamina;
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack && Stamina >15)
            {
                SwordAttack();         
            
            }
                
        }


    }

    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("attack");
        StartCoroutine(ResetAttackBool());
    
    }


    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
        CanAttack = true;
    }

}
