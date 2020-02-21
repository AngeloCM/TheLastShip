using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI.EnemyCode
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]

    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        EnemyFlyPoint[] _flyPoints;

        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;



        public void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }

        public EnemyFlyPoint[] FlyPoints
        {
            get
            {
                return _flyPoints;
            }
        }
    }
}
