using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;  // Speed of the bullet
    public int damage = 50;    // Damage the bullet deals
    private Transform target;  // Target the bullet will move towards

    // Function to set the bullet's target
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // If the target is null, destroy the bullet to avoid memory leakage
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate direction to the target
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if the distance to the target is less than the distance we can move in one frame
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the bullet towards the target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    // Method to handle what happens when the bullet hits the target
    void HitTarget()
    {
        Damage(target);
        Destroy(gameObject);  // Destroy the bullet after hitting the target
    }

    // Method to apply damage to the target
    void Damage(Transform enemy)
    {
        EnemyAi e = enemy.GetComponent<EnemyAi>();
        if (e != null)
        {
            e.TakeDamage(damage);  // Call the TakeDamage method on the enemy
        }
    }
}
