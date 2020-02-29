﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI.EnemyCode
{
    [RequireComponent(typeof(FiniteStateMachine))]

    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        EnemyFlyPoint[] _flyPoints;

        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;

        [SerializeField, Tooltip("Reference to the Player")]
        public GameObject PlayerReference;

        [SerializeField, Tooltip("The velocity of the enemy")]
        public float movSpeed = 5f;

        [SerializeField, Tooltip("The time to wait in Idle State")]
        public float totalDurationIdle = 2f;

        [SerializeField, Tooltip("The distance between the Player and Enemy to Atack the Player")]
        public float DistanceToAttackPlayer = 20f;

        public void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
            PlayerReference = GameObject.FindGameObjectWithTag("Player");
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
