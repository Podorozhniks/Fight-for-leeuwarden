using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    //public GameObject HitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wc.IsAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("hit");

            // Instantiate(HitParticle, new Vector3(other.transform.position.x,
            //    transform.position.y,other.transform.position.z),
            //   other.transform.rotation);

            // Trigger controller rumble
            TriggerRumble(0.5f, 0.5f, 0.5f);
        }
    }

    private void TriggerRumble(float lowFrequency, float highFrequency, float duration)
    {
        if (Gamepad.current != null)
        {
            Debug.Log("TriggerRumble called");
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(StopRumble(duration));
        }
        else
        {
            Debug.Log("No gamepad connected");
        }
    }

    private IEnumerator StopRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (Gamepad.current != null)
        {
            Debug.Log("StopRumble called");
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}
