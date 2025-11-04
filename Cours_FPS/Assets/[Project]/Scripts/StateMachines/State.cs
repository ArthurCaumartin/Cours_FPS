public class State
{
    protected StateMachine stateMachine;
    public virtual void Init(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void EnterState() { }
}