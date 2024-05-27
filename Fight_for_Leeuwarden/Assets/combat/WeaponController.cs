using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

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

    public InputAction attackAction; // Define an InputAction for attack
    public InputAction blockAction; // Define an InputAction for block
    public InputAction bashAction; // Define an InputAction for shield bash

    void OnEnable()
    {
        // Enable the input actions
        attackAction.Enable();
        blockAction.Enable();
        bashAction.Enable();
    }

    void OnDisable()
    {
        // Disable the input actions
        attackAction.Disable();
        blockAction.Disable();
        bashAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Stamina = stamina.Stamina;
        // Register the callbacks for the input actions
        attackAction.performed += context => OnAttack();
        blockAction.performed += context => OnBlock();
        blockAction.canceled += context => OnBlockEnd();
        bashAction.performed += context => OnBash();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShieldUp)
        {
            Animator anim = Shield.GetComponent<Animator>();
            anim.ResetTrigger("block");
            anim.SetTrigger("idle");
            CanAttack = true;
            BlockedDamageMultiplier = 1f;
        }
        else
        {
            Animator anim2 = Shield.GetComponent<Animator>();
            anim2.SetTrigger("block");
            BlockedDamageMultiplier = 0.1f;
            CanAttack = false;
        }

        Stamina = stamina.Stamina;
    }

    private void OnAttack()
    {
        if (CanAttack && Stamina > 15)
        {
            SwordAttack();
        }
    }

    private void OnBlock()
    {
        if (Stamina > 5)
        {
            ShieldUp = true;
        }
    }

    private void OnBlockEnd()
    {
        ShieldUp = false;
    }

    private void OnBash()
    {
        if (ShieldUp && Stamina > 10 && CanBash)
        {
            ShieldBash();
            CanBash = false;
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







