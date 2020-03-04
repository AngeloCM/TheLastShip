using Assets.Scripts.AI;
using Assets.Scripts.AI.EnemyCode;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED
};

public enum FSMStateType
{
    IDLE,
    FLY,
    ATTACK,
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected Enemy _enemy;
    protected FiniteStateMachine _fsm;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool sucessEnemy = true;
        ExecutionState = ExecutionState.ACTIVE;

        //Does the executing agent exist?
        sucessEnemy = (_enemy != null);

        return sucessEnemy;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if (fsm != null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(Enemy enemy)
    {
        if (enemy != null)
        {
            _enemy = enemy;
        }
    }
}
