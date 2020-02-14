using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemy : AI_Enemy
{
    [SerializeField, Tooltip("The enemy's health")]
    public float Swarm_health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fly()
    {
        base.Fly();
    }
}
