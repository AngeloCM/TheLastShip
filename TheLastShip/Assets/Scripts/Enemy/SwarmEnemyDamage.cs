using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemyDamage : MonoBehaviour
{

    [SerializeField, Tooltip("The damage this will deal to the player or cargo ship on impact. For reference, player health is about 100.")]
    private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag == "CargoShip" || collision.transform.root.tag == "Player")
        {
            collision.gameObject.GetComponent<DamageHandler>().TakeDamage(damage);

            this.GetComponent<EnemyHealth>().CurrentHealth = 0; // will kill this
        }
    }
}
