using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public stamina stamina;
    public float Stamina = 50;
    public bool CanAttack = true;
    public float AttackCooldown = 0.01f;
    public bool IsAttacking = false;

    public InputAction attackAction; // Define an InputAction for attack

    void OnEnable()
    {
        // Enable the attack action
        attackAction.Enable();
    }

    void OnDisable()
    {
        // Disable the attack action
        attackAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Stamina = stamina.Stamina;
        // Register the callback for the attack action
        attackAction.performed += context => OnAttack();
    }

    // Update is called once per frame
    void Update()
    {
        Stamina = stamina.Stamina;
    }

    private void OnAttack()
    {
        if (CanAttack && Stamina > 15)
        {
            SwordAttack();
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






