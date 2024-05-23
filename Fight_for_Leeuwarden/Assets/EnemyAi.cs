using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform tower;

    public LayerMask whatIsGround, whatIsPlayer, whatIsTower;

    public GameObject MainTower;

    public float health;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    /// public float timeBetweenAttacks;
    /// <summary>
    /// public float timeBetweenAttacks;
    /// </summary>c GameObject projectile;

    // States
    public float sightRange, attackRange, towerRange;
    public bool playerInSightRange, playerInAttackRange, towerInSightRange;

    private void Awake()
    {
        tower = GameObject.Find("TowerObj").transform;
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        towerInSightRange = Physics.CheckSphere(transform.position, towerRange, whatIsTower);

        if (!playerInSightRange && !playerInAttackRange) Assaulting();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    }

    private void Assaulting()
    {
        agent.SetDestination(tower.position);
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

