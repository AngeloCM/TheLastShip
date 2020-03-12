using Assets.Scripts.AI.EnemyCode;
using Assets.Scripts.AI.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public class FiniteStateMachine 
    {
        AbstractFSMState _currentState;

        List<AbstractFSMState> _validStates;

        Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        Enemy enemy;

        public FiniteStateMachine(Enemy enemy)
        {
            this.enemy = enemy;


            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            //E = this.GetComponent<Enemy>();
            this._validStates = new List<AbstractFSMState>() { new IdleState(), new FlyState(), new AttackState()};

            foreach (AbstractFSMState state in _validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(enemy);
                _fsmStates.Add(state.StateType, state);

                EnterState(FSMStateType.IDLE);
            }
        }

        
        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        #region STATE MANAGEMENT

        public void EnterState(AbstractFSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            
            _currentState = nextState;
            _currentState.EnterState();
        }

        public void EnterState(FSMStateType stateTytpe)
        {
            if (_fsmStates.ContainsKey(stateTytpe))
            {
                AbstractFSMState nextState = _fsmStates[stateTytpe];

                EnterState(nextState);
            }
        }

        #endregion
    }
}
