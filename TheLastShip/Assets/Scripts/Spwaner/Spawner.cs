﻿using Assets.Scripts.AI.EnemyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Spwaner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField, Tooltip("Reference to SwarmEnemy Prefab")]
        public GameObject SwarmEnemy_prefab;

        [SerializeField, Tooltip("The amount of Enemies you want to respawn")]
        public int AmountOfEnemies = 5;


        [SerializeField]
        GameObject[] Spawn;

        int SpawnIndex;

        private void Start()
        {
            SpawnEnemy();
        }

        void SpawnEnemy()
        {
            for (int i = 0; i < AmountOfEnemies; i++)
            {
                SpawnIndex = UnityEngine.Random.Range(0, Spawn.Length);
                GameObject enemy = Instantiate(SwarmEnemy_prefab, Spawn[SpawnIndex].transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().enabled = true;
            }        
        }
    }
}
