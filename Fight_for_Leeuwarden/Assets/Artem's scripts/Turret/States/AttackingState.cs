using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : MinerState
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawnPosition;

    private IEnumerator ShootProjectile()
    {
        Instantiate(_projectile, _projectileSpawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ShootProjectile());
    }

    private void OnEnable()
    {
        StartCoroutine(ShootProjectile());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}





