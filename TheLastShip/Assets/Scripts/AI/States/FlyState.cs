using Assets.Scripts.AI.EnemyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.States
{
    [CreateAssetMenu(fileName = "FlyState", menuName = "Unity-FSM/States/Fly", order = 2)]

    public class FlyState : AbstractFSMState
    {
        EnemyFlyPoint[] _flyPoints;
        int _flyPointIndex;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.FLY;
            _flyPointIndex = -1;
        }

        public override bool EnterState()
        {
            EnteredState = false;

            if (base.EnterState())
            {
                _flyPoints = _enemy.FlyPoints;

                if (_flyPoints == null || _flyPoints.Length == 0)
                {
                    Debug.Log("FlyState : Failed to grab fly points from the Enemy");
                    
                }
                else
                {
                    if (_flyPointIndex < 0)
                    {
                        _flyPointIndex = UnityEngine.Random.Range(0, _flyPoints.Length);
                    }
                    else
                    {
                        _flyPointIndex = (_flyPointIndex + 1) % _flyPoints.Length;
                    }

                    SetDestination(_flyPoints[_flyPointIndex]);
                    EnteredState = true;
                }
            }

            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                //Logic
                if (Vector3.Distance(_navMeshAgent.transform.position, _flyPoints[_flyPointIndex].transform.position) <= 1f)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
            }
        }

        private void SetDestination(EnemyFlyPoint destination)
        {
            if (_navMeshAgent != null && destination != null)
            {
                _navMeshAgent.SetDestination(destination.transform.position);
            }
        }
    }
}
