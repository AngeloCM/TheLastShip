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

        GameObject[] allWaypoints;

        FiniteStateMachine _finiteStateMachine;

        [SerializeField, Tooltip("Reference to the Player")]
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
            PlayerReference = GameObject.FindGameObjectWithTag("Player");
            
        }

        public void Start()
        {
            allWaypoints = GameObject.FindGameObjectsWithTag("FlyPoint");
            AttachFlyPoints();
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

        public void AttachFlyPoints()
        {
            for (int i = 0; i < allWaypoints.Length; i++)
            {
                _flyPoints[i] = allWaypoints[i].GetComponent<EnemyFlyPoint>();
            }
        }
    }
}
