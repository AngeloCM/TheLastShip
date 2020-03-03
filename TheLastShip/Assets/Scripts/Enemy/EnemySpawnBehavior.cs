using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour
{
    [SerializeField, Tooltip("The prefab for the enemy to be spawned.")]
    private GameObject enemyPrefab;

    [SerializeField, Tooltip("The number of enemies to spawn in one \"spawn\".")]
    private int numOfEnemiesPerSpawn;

    [SerializeField, Tooltip("The number of times to spawn a wave of enemies before stopping.")]
    private int numOfSpawns;

    [SerializeField, Tooltip("The time in seconds between enemy spawns.")]
    private float spawnCooldown;

    private float spawnTimer;

    private int wavesSpawned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnEnemies();
    }

    private void UpdateSpawnEnemies()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnCooldown)
        {
            if (wavesSpawned < numOfSpawns)
            {
                for (int i = 0; i < numOfEnemiesPerSpawn; i++)
                {
                    IndicatorScript.TargetList.Add(Instantiate(enemyPrefab, this.transform.position + (UnityEngine.Random.onUnitSphere * 20), this.transform.rotation));
                }

                wavesSpawned++;
            }

            spawnTimer = 0f;
        }
    }
}
