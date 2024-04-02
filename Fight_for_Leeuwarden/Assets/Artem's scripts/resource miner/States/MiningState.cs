using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningState : MinerState
{
    private ResourceCounter _resourceCounter;

    private void OnEnable()
    {
        _resourceCounter = ResourceCounter.Instance;
        StartCoroutine(Mine());
    }

    private IEnumerator Mine()
    {
        yield return new WaitForSeconds(2);
        _resourceCounter.RecieveResources(1);
        StartCoroutine(Mine());
    }
}
