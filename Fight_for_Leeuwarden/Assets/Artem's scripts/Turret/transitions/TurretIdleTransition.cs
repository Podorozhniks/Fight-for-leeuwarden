using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleTransition : MinerTransition
{
    private LineController _lineController;

    private WaveSpawner _waveSpawner;

    private void OnEnable()
    {
        _waveSpawner = WaveSpawner.Instance;
        _lineController = _waveSpawner.LineControllers[(int)(transform.position.z / 2)];
    }

    private void Update()
    {
        if (_lineController.EnemiesAlive == 0)
            NeedSwitch = true;
    }
}
