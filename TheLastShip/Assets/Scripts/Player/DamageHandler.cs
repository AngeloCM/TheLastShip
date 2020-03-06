using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField, Tooltip("The HealthBar script component representing this ship's health.")]
    private HealthBar healthBar;

    [SerializeField, Tooltip("The ShieldBar script component representing this ship's shields.")]
    private ShieldBar shieldBar;

    // Take damage. Should be called when hit with a projectile, etc.
    public void TakeDamage(int dmg)
    {
        if (shieldBar != null)
        {
            if (shieldBar.CheckIfShieldDelpeted())
            {
                healthBar.Damage(dmg);
            }
            else
            {
                shieldBar.DamageShield(dmg);

                if (this.gameObject.GetComponent<PlayerAudioTriggers>() != null)
                {
                    // audio ckrueger vvv
                    this.gameObject.GetComponent<PlayerAudioTriggers>().PlaySoundDamageShield();
                    // audio ckrueger ^^^
                }
                else if (this.gameObject.GetComponent<CargoAudioTriggers>() != null)
                {
                    // Cole sound trigger for cargo ship getting hit
                }
            }
        }
        else
        {
            if (healthBar != null)
            {
                healthBar.Damage(dmg);
            }
        }

        // Kill by calling DeathLossTrigger.Die on this game object if health <= 0
        // Note that this game object must have a DeathLossTrigger
        if (healthBar != null)
        {
            if (healthBar.gameObject.GetComponent<BarManager>().GetCurrentValue() <= 0)
            {

                this.gameObject.GetComponent<ExplosionPlayer>().CreateExplosion();

                this.GetComponent<DeathLossTrigger>().Die();
            }
        }
    }
}
