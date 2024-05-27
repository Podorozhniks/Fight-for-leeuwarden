using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Shield;
    public stamina stamina;
    public float Stamina = 50;
    public float BlockedDamageMultiplier = 1f;
    public bool CanAttack = true;
    public bool CanBash = true;
    public float AttackCooldown = 0.01f;
    public bool IsAttacking = false;
    public bool ShieldUp = false;


    // Start is called before the first frame update
    void Start()
    {
        Stamina = stamina.Stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            ShieldUp = false;
        }

        if (Input.GetMouseButton(1) && Stamina > 5)
        {
            ShieldUp = true;
        }
 

        switch (ShieldUp) {
            case false:
                Animator anim = Shield.GetComponent<Animator>();
                anim.ResetTrigger("block");
                anim.SetTrigger("idle");
                CanAttack = true;
                BlockedDamageMultiplier = 1f;

                break;
            case true:
                Animator anim2 = Shield.GetComponent<Animator>();
               // anim2.ResetTrigger("idle");
                //anim2.SetTrigger("block");
                BlockedDamageMultiplier = 0.1f;
                if (CanBash = true)
                {
                    Debug.Log("can bash");
                    anim2.ResetTrigger("idle");
                    anim2.SetTrigger("block");
                }
                CanAttack = false;

                if (Input.GetMouseButtonDown(0) && Stamina > 10 && CanBash) 
                {
                    ShieldBash();
                    CanBash = false;
                }

                break; }


        Stamina = stamina.Stamina;
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack && Stamina > 15)
            {
                SwordAttack();

            }

        }


    }
    public void ShieldBash()
    {
        IsAttacking = true;
        CanBash = false;
        Animator anim = Shield.GetComponent<Animator>();
        anim.ResetTrigger("block");
        anim.SetTrigger("bash");
        StartCoroutine(ResetAttackBool2());
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
        yield return new WaitForSeconds(0.2f);
        IsAttacking = false;
        CanAttack = true;
    }

    IEnumerator ResetAttackBool2()
    {
        yield return new WaitForSeconds(0.2f);
        IsAttacking = false;
        CanBash = true;
    }

}
