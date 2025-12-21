
using Unity.VisualScripting;
using UnityEngine;

public class StateMachineMob_Rusher : StateMachine_MobBehavior
{
    [SerializeField] private Explosif _explosif;
    [SerializeField] private State_MobRoam _stateMobRoam;
    [SerializeField] private State_RushKamikaze _stateMobRush;

    protected override void Awake()
    {
        base.Awake();

        _explosif = GetComponent<Explosif>();

        _stateMobRoam.Init(this, navMeshAgent);
        _stateMobRush.Init(this, navMeshAgent, targetFinder, _explosif);
        SetCurrentState(_stateMobRoam);
    }

    protected override void Update()
    {
        base.Update();
        if (!targetFinder.Target)
            SetCurrentState(_stateMobRoam);
        else
            SetCurrentState(_stateMobRush);
    }
}
