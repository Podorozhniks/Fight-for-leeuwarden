using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private float spawnDelay = 1f; 
    [SerializeField] private int numberOfEnemies = 5; 
    private int currentEnemiesSpawned = 0; 
    private Coroutine spawningCoroutine;

    void Start()
    {
        
        spawningCoroutine = StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemiesSpawned < numberOfEnemies)
        {
            
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentEnemiesSpawned++;

         
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Stop Spawning
    public void StopSpawning()
    {
        if (spawningCoroutine != null)
        {
            StopCoroutine(spawningCoroutine);
        }
    }
}
