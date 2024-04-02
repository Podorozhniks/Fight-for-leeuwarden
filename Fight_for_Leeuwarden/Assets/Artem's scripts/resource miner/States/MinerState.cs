using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinerState : MonoBehaviour
{
    [SerializeField] private MinerTransition[] _transitions;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;

            foreach (var transition in _transitions)
                transition.enabled = true;
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public MinerState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            Debug.Log(transition.NeedSwitch);
            if (transition.NeedSwitch)
            {
                return transition.NextState;
            }
        }

        return null;
    }
}
