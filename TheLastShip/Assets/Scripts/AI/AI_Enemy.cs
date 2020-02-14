using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("The radius of the enemy")]
    protected float EnemyRadius = 10f;

    [SerializeField, Tooltip("The speed of the enemy")]
    protected float EnemySpeed = 5f;

    protected Transform EnemyTransform;
    protected Transform PlayerTranform;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTranform = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Fly()
    {
        
    }

    public virtual void Suicide()
    {

    }

    public virtual void Shoot()
    {

    }
}
