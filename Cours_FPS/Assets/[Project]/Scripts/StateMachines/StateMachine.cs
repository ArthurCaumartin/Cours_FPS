
using Alchemy.Inspector;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField, ReadOnly] private string _currentStateName = "null";
    [Space]
    private State _currentState = null;

    protected virtual void Update()
    {
        _currentState?.UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        _currentState?.FixedUpdateState();
    }

    public virtual void SetCurrentState(State newState)
    {
        if (!newState || _currentState == newState)
            return;

        _currentState?.ExitSate();
        newState?.EnterState();

        _currentState = newState;
        _currentStateName = !_currentState ? "null" : _currentState.ToString();
    }
}
