using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseCustomer : MonoBehaviour, ICustomerable
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    
    private List<Transform> _myRoot;
    private int _currentRootIndex = 0;
    
    public virtual void OnReady(List<Transform> rootInfos)
    {
        _myRoot = rootInfos;
        _currentRootIndex = 0;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.isStopped = true;
    }

    public virtual void OnStart()
    {
        _navMeshAgent.SetDestination(_myRoot[_currentRootIndex].position);
        _currentRootIndex += 1;
        
        _navMeshAgent.isStopped = false;
    }

    public virtual void OnEnd()
    {
        GameManager.Instance.ClearCustomer();
        Destroy(gameObject);
    }

    public virtual void OnAnimationTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
