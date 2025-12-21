using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable]
public class State_MobRoam : State
{
    [SerializeField] private float _newPositionPickDelay = 5;
    [SerializeField] private float _newPositioMaxRange = 5;
    private float _positionPickTimer;
    private Vector3 _roamPosition;
    private NavMeshAgent _navMeshAgent;

    public override void EnterState()
    {
        base.EnterState();
        _navMeshAgent.SetDestination(stateMachine.transform.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _positionPickTimer += Time.deltaTime;
        if (_positionPickTimer > _newPositionPickDelay)
        {
            _positionPickTimer = 0;
            PickNewPosition();
        }
    }

    private void PickNewPosition()
    {
        _roamPosition = stateMachine.transform.position
                        + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * Random.Range(_newPositioMaxRange * .5f, _newPositioMaxRange);
        _navMeshAgent.SetDestination(_roamPosition);
    }

    public virtual void Init(StateMachine stateMachine, NavMeshAgent navMeshAgent)
    {
        base.Init(stateMachine);
        this._navMeshAgent = navMeshAgent;
    }
}