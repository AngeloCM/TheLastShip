using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI.States
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "Unity-FSM/States/Attack", order = 3)]

    public class AttackState : AbstractFSMState
    {
        float updateTime = 0f;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.ATTACK;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();

            if (EnteredState)
            {
                Debug.Log("ENTERED ATTACK STATE");
                updateTime = 0;
            }

            return EnteredState;
        }

        public override void UpdateState()  
        {
            if (EnteredState)
            {
                updateTime += Time.deltaTime;

                if (Vector3.Distance(_enemy.transform.position, _enemy.PlayerReference.transform.position) > _enemy.DistanceToAttackPlayer)
                {
                    _fsm.EnterState(FSMStateType.FLY);
                }
                else if (Vector3.Distance(_enemy.transform.position, _enemy.PlayerReference.transform.position) < _enemy.DistanceToExplode)
                {
                    Destroy(_enemy.gameObject);
                }
                else
                {
                    AttackPlayer();
                }
            }
        }

        private void AttackPlayer()
        {
            Debug.Log("ATTACKING!");
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.PlayerReference.transform.position, _enemy.movSpeed * updateTime);
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("EXITING ATTACK STATE");
            return true;
        }

    }
}
