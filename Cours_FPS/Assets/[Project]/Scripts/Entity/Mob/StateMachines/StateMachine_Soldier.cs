
using UnityEngine;

public class StateMachine_Soldier : StateMachine_MobBehavior
{
    [SerializeField] private State_MobRoam _stateMobRoam;

    protected override void Awake()
    {
        base.Awake();

        _stateMobRoam.Init(this, navMeshAgent);
        SetCurrentState(_stateMobRoam);
    }



}
