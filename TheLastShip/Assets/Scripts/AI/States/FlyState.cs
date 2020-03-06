﻿using Assets.Scripts.AI.EnemyCode;
using System;
using System.Collections;
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
        float updateTime;

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

                    EnteredState = true;
                } 
            }

            if (EnteredState)
            {
                Debug.Log("ENTERED FLY STATE " + _enemy.gameObject.name);
                updateTime = 0f;
            }

            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                updateTime += Time.deltaTime;

                //Logic
                if (Vector3.Distance(_enemy.transform.position, _flyPoints[_flyPointIndex].transform.position) <= _enemy.DistanceToTouchWaypoint)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
                else if (Vector3.Distance(_enemy.transform.position, _enemy.PlayerReference.transform.position) <= _enemy.DistanceToAttackPlayer)
                {
                    _fsm.EnterState(FSMStateType.ATTACK);
                }
                else
                {
                    SetDestination(_flyPoints[_flyPointIndex]);
                }
            }
        }

        private void SetDestination(EnemyFlyPoint destination)
        {
            Debug.Log("FLYING");
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, destination.transform.position, _enemy.movSpeed * updateTime);
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("EXITING FLY STATE");
            return true;
        }
    }
}
