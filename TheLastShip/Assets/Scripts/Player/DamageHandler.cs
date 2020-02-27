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

                // audio ckrueger vvv
                PlaySoundDamageShield();
                // audio ckrueger ^^^
            }
        }
        else
        {
            healthBar.Damage(dmg);
        }

        // Kill by calling DeathLossTrigger.Die on this game object if health <= 0
        // Note that this game object must have a DeathLossTrigger
        if (healthBar.gameObject.GetComponent<BarManager>().GetCurrentValue() <= 0)
        {

            this.gameObject.GetComponent<ExplosionPlayer>().CreateExplosion();

            this.GetComponent<DeathLossTrigger>().Die();
        }
    }

    // audio ckrueger vvv
    public void PlaySoundDamageShield()
    {
        AkSoundEngine.PostEvent("plr_shield_damage", gameObject);
    }
    // audio ckrueger ^^^
}
