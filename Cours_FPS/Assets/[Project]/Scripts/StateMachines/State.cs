using System;
using System.ComponentModel.Design;
using UnityEngine;

[Serializable]
public abstract class State
{
    protected StateMachine stateMachine;
    protected Transform transform;

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void FixedUpdateState() { }
    public virtual void ExitSate() { }

    public virtual void Init(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        transform = stateMachine.transform;
    }

    public static bool operator !(State state)
    {
        return state == null;
    }
}
