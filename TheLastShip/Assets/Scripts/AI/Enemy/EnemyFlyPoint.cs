using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.EnemyCode
{
    public class EnemyFlyPoint : MonoBehaviour
    {
        [SerializeField]
        protected float debugDrawRadius = 1.0f;

        [SerializeField]
        protected float _connectivityRadius = 50f;

        List<EnemyFlyPoint> _connections;

        public void Start()
        {
            //Garb all waypoiunts objects in scene
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("FlyPoint");

            //Create a list of waypoints I can refer to later.
            _connections = new List<EnemyFlyPoint>();

            for (int i = 0; i < allWaypoints.Length; i++)
            {
                EnemyFlyPoint nextWaypoint = allWaypoints[i].GetComponent<EnemyFlyPoint>();

                //We found a waypoint
                if (nextWaypoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                    {
                        _connections.Add(nextWaypoint);
                    }
                }
            }
        }

        void Update()
        {
            foreach (var point in _connections)
            {
                point.transform.position = point.transform.position;
            }
        }

        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
        }
    }
}
