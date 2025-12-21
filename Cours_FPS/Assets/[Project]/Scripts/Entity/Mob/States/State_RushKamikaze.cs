
using System;
using UnityEngine.AI;

[Serializable]
public class State_RushKamikaze : State
{
    private NavMeshAgent _navMeshAgent;
    private TargetFinder _targetFinder;
    private Explosif _explosif;

    public override void UpdateState()
    {
        if (!_targetFinder.Target) return;
        base.UpdateState();
        _navMeshAgent.SetDestination(_targetFinder.Target.position);

        float distance = (_targetFinder.Target.position - transform.position).magnitude;
        _explosif.TryExplodeWithDistance(distance);
    }

    public virtual void Init(StateMachine stateMachine, NavMeshAgent navMeshAgent, TargetFinder targetFinder, Explosif explosif)
    {
        base.Init(stateMachine);
        _navMeshAgent = navMeshAgent;
        _targetFinder = targetFinder;
        _explosif = explosif;
    }
}
