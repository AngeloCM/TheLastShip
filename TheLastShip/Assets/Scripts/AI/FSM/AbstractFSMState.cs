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
    FLY    
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent _navMeshAgent;
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
        bool sucessNavMesh = true;
        bool sucessEnemy = true;
        ExecutionState = ExecutionState.ACTIVE;

        //Does the nav mesh agent exist?
        sucessNavMesh = (_navMeshAgent != null);

        //Does the executing agent exist?
        sucessEnemy = (_enemy != null);

        return sucessNavMesh & sucessEnemy;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
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
