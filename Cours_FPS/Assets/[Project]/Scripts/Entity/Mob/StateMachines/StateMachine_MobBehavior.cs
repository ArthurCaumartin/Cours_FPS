
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(TargetFinder))]
public abstract class StateMachine_MobBehavior : StateMachine
{
    protected NavMeshAgent navMeshAgent;
    protected TargetFinder targetFinder;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetFinder = GetComponent<TargetFinder>();
    }
}
