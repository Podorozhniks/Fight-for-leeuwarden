using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stamina : MonoBehaviour
{
    public WeaponController WeaponController;
    public float Stamina = 50;
    public bool IsAttacking = false;
    public bool Attacked = false;
    public float MaxStamina = 50;


    // Start is called before the first frame update
    void Start()
    {
        IsAttacking = WeaponController.IsAttacking;
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking = WeaponController.IsAttacking;
        if (IsAttacking == false)
        {
            Stamina= Stamina + 1f;
            Attacked = false;
        }
        else if (IsAttacking == true && Attacked == false)
        {
            Stamina = Stamina - 15;
            Attacked = true;
        }

        if (Stamina > MaxStamina) 
        { 
            Stamina = MaxStamina;
        }
    }
}
