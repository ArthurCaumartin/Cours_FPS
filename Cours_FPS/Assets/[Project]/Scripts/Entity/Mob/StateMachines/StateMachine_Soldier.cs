
using UnityEngine;

public class StateMachine_Soldier : StateMachine_MobBehavior
{
    [SerializeField] private WeaponBehavior _weapon;
    [SerializeField] private State_MobRoam _stateMobRoam = new State_MobRoam();
    [SerializeField] private State_ShootOnTarget _stateShootOnTarget = new State_ShootOnTarget();

    protected override void Awake()
    {
        base.Awake();

        _stateMobRoam.Init(this, navMeshAgent);
        _stateShootOnTarget.Init(this, _weapon, targetFinder, navMeshAgent);
        SetCurrentState(_stateMobRoam);
    }

    protected override void Update()
    {
        base.Update();

        if (targetFinder.Target == null)
            SetCurrentState(_stateMobRoam);
        else
            SetCurrentState(_stateShootOnTarget);

    }

}
