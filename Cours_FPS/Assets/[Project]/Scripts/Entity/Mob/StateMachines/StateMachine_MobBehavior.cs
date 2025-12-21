
using UnityEngine;
using UnityEngine.AI;

public class StateMachine_MobBehavior : StateMachine
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected TargetFinder targetFinder;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetFinder = GetComponent<TargetFinder>();
    }
}
