using Assets.Scripts.AI.EnemyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public class FiniteStateMachine : MonoBehaviour
    {
        AbstractFSMState _currentState;

        [SerializeField]
        List<AbstractFSMState> _validStates;

        Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        public void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();
            Enemy enemy = this.GetComponent<Enemy>();

            foreach(AbstractFSMState state in _validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(enemy);
                _fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            EnterState(FSMStateType.IDLE);
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
