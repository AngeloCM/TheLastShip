using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaceholderShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShot;

    [SerializeField]
    private GameObject shotSpawn;

    private float shotCooldown = 0.5f;
    private float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        if (shotTimer >= shotCooldown)
        {
            SpawnShot();

            shotTimer = 0f;
        }
    }

    private void SpawnShot()
    {
        GameObject shot = Instantiate(enemyShot, shotSpawn.transform);

        shot.GetComponent<BasicShot>().shotSource = BasicShot.ShotSources.enemy;
    }
}
