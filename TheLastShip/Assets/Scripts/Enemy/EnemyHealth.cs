using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum health this enemy has. Compare to player damage and edit this value on an enemy prefab."), Range(1, 500)]
    public int MaxHealth;

    [HideInInspector]
    public int CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            // todo: Die -- should have animation/explosion/etc.

            this.gameObject.GetComponent<ExplosionPlayer>().CreateExplosion();
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth -= dmg;
    }
}
