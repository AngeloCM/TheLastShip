using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaceholderAI : MonoBehaviour
{
    private Rigidbody rb;

    private GameObject playerRef;

    private Vector3 originPoint;

    [SerializeField, Tooltip("The speed in units/sec this enemy will move while flying.")]
    private float moveSpeed;

    [SerializeField, Tooltip("The damage this will deal to the player or cargo ship on impact. For reference, player health is about 100.")]
    private int damage;

    [SerializeField, Tooltip("The maximum distance from the spawn point the enemy will wander before turning around.")]
    private float maxWanderDistance;

    [SerializeField, Tooltip("The maximum distance from the player this enemy will start homing in and attacking.")]
    private float maxAttackDistance;

    private PlaceholderEnemyState.PlaceholderEnemyStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        currentState = PlaceholderEnemyState.PlaceholderEnemyStates.idle;

        originPoint = this.transform.position;

        playerRef = GameObject.FindGameObjectWithTag("Player");
    }
    
    // FixedUpdate
    void FixedUpdate()
    {
        switch (currentState)
        {
            case PlaceholderEnemyState.PlaceholderEnemyStates.idle:
                UpdateBehaviorIdle();
                break;
            case PlaceholderEnemyState.PlaceholderEnemyStates.flyingStraight:
                UpdateBehaviorFlyStraight();
                break;
            case PlaceholderEnemyState.PlaceholderEnemyStates.attacking:
                UpdateBehaviorAttack();
                break;
        }

        UpdateChangeState();
    }

    private void UpdateChangeState()
    {
        switch (currentState)
        {
            case PlaceholderEnemyState.PlaceholderEnemyStates.idle: // Nothing special here, just always switch to flying state
                currentState = PlaceholderEnemyState.PlaceholderEnemyStates.flyingStraight;
                //Debug.Log(this.name + " state flying");
                break;
            case PlaceholderEnemyState.PlaceholderEnemyStates.flyingStraight: // Switch from fly to attack if within range
                if (Vector3.Distance(playerRef.transform.position, this.transform.position) < maxAttackDistance)
                {
                    currentState = PlaceholderEnemyState.PlaceholderEnemyStates.attacking;

                    //Debug.Log(this.name + " state attacking");
                }
                break;
            case PlaceholderEnemyState.PlaceholderEnemyStates.attacking: // Switch from attack to fly if outside range
                if (Vector3.Distance(playerRef.transform.position, this.transform.position) > maxAttackDistance)
                {
                    currentState = PlaceholderEnemyState.PlaceholderEnemyStates.flyingStraight;

                    //Debug.Log(this.name + " state flying");
                }
                break;
        }


        
    }

    private void UpdateBehaviorAttack()
    {
        Vector3 directionOfPlayer = Vector3.Normalize(playerRef.transform.position - this.transform.position);

        this.transform.forward = Vector3.RotateTowards(this.transform.forward, Vector3.Lerp(this.transform.forward, directionOfPlayer, 0.5f), Vector3.Angle(this.transform.forward, directionOfPlayer), 1f);

        rb.velocity = Vector3.Lerp(rb.velocity, this.transform.forward * moveSpeed, 0.02f);
    }

    // Fly straight until x units from origin, then turn around
    private void UpdateBehaviorFlyStraight()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, this.transform.forward * moveSpeed, 0.02f);

        if (Vector3.Distance(this.transform.position, originPoint) > maxWanderDistance)
        {
            // turn around (every now and then I fall apaaaaaaaaart)
            this.gameObject.transform.rotation = Quaternion.LookRotation(originPoint - this.transform.position);

            Debug.Log("I turned around");
        }
    }

    // Slow to a stop
    private void UpdateBehaviorIdle()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.02f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag == "CargoShip" || collision.transform.root.tag == "Player")
        {
            collision.gameObject.GetComponent<DamageHandler>().TakeDamage(damage);

            this.GetComponent<EnemyHealth>().CurrentHealth = 0; // will kill this
        }
    }
}
