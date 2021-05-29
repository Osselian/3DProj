using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaredEnabled : StateMachineBehaviour
{
    private UnityEvent _runAway = new UnityEvent();

    public event UnityAction RunAway
    {
        add => _runAway.AddListener(value);
        remove => _runAway.RemoveListener(value);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _runAway?.Invoke();
    }
}
