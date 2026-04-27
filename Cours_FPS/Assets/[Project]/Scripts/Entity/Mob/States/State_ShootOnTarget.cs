using System;
using UnityEngine;
using UnityEngine.AI;

// se deplace vers la target
// si la target est en ligne de vue
// piou piou :)
public class State_ShootOnTarget : State
{
    private WeaponBehavior _weaponBehavior;
    private TargetFinder _targetFinder;
    private NavMeshAgent _navMeshAgent;
    public virtual void Init(StateMachine stateMachine, WeaponBehavior weaponBehavior, TargetFinder targetFinder, NavMeshAgent navMeshAgent)
    {
        base.Init(stateMachine);
        this._weaponBehavior = weaponBehavior;
        this._targetFinder = targetFinder;
        this._navMeshAgent = navMeshAgent;

        _weaponBehavior.InitGrab(null, null);
    }

    public override void EnterState()
    {
        base.EnterState();
        _navMeshAgent.SetDestination(transform.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_targetFinder.Target)
        {
            _weaponBehavior.transform.forward =
            (_targetFinder.Target.position - transform.position).normalized;

            _weaponBehavior.Shoot(true);
            if(_weaponBehavior.IsMagazinEmpty)
                _weaponBehavior.Reload();
        }

    }


    public override void ExitSate()
    {
        base.ExitSate();
        _weaponBehavior.transform.forward = transform.forward;
    }



}