using System;
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
        [SerializeField, Tooltip("All waypoints that the enemy will follow.")]
        public EnemyFlyPoint[] _flyPoints;

        EnemyFlyPoint EnemyFlyPointReference;

        FiniteStateMachine _finiteStateMachine;

        public GameObject PlayerReference;

        [SerializeField, Tooltip("The velocity of the enemy")]
        public float movSpeed = 100f;

        [SerializeField, Tooltip("The time to wait in Idle State")]
        public float totalDurationIdle = 2f;

        [SerializeField, Tooltip("The distance between the Waypoint and the Enemy, so the enemy understand that he reached the waypoint.")]
        public float DistanceToTouchWaypoint = 2f;

        [SerializeField, Tooltip("The distance between the Player and Enemy to Atack the Player")]
        public float DistanceToAttackPlayer = 300f;

        [SerializeField, Tooltip("The distance between the Player and Enemy to Explode Enemy on Player")]
        public float DistanceToExplode = 50f;

        public void Awake()
        {
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        void Start()
        {
            PlayerReference = GameObject.FindGameObjectWithTag("Player");
            EnemyFlyPointReference = GameObject.FindGameObjectWithTag("FlyPoint").GetComponent<EnemyFlyPoint>();
            AttachAllWaypoints();
        }

        void Update()
        {
            PlayerReference.transform.position = PlayerReference.transform.position;
        }

        public EnemyFlyPoint[] FlyPoints
        {
            get
            {
                return _flyPoints;
            }
        }

        void AttachAllWaypoints()
        {
            for (int i = 0; i < _flyPoints.Length; i++)
            {
                _flyPoints[i] = EnemyFlyPointReference._connections.ElementAt(i).GetComponent<EnemyFlyPoint>();
            }
        }
    }
}
