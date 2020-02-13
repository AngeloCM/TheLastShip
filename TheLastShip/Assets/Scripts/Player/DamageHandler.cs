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

    [SerializeField, Tooltip("The damage this ship takes when it collides with something."), Range(0, 100)]
    private int KnockbackDamage = 4;

    [SerializeField, Tooltip("The speed at which this ship is knocked away when it collides with something."), Range(0f, 50f)]
    private float KnockbackSpeed = 14f;

    private Rigidbody rb;
    private PlayerController pCont;
    private PlayerShipMovement pShipMov;

    private float knockbackSlowingRate = 0.99f; // The rate at which the knockback decreases. Closer to 1 = slower; closer to 0 = faster

    private float knockbackTime = 1f; // The time the player is in knockback for
    private float knockbackTimer = 0f; // The active timer for knockback

    private bool isInKnockback; // Whether we're in knockback. Used by update to determine when to give back control

    private bool isInKnockbackInvincibility; // Whether the player is in invincibility following knockback. This handles cases where the player
                                             // gets ping-ponged around, and should be treated as separate from shooting damage.

    private Vector3 collisionNormal;

    private void Start()
    {
        pCont = this.GetComponent<PlayerController>();

        rb = this.GetComponent<Rigidbody>();

        pShipMov = this.GetComponent<PlayerShipMovement>();

        isInKnockback = false;
        isInKnockbackInvincibility = false;
    }

    private void Update()
    {
        if (isInKnockback) // Do knockback stuff every frame, in update, if being knocked back
        {
            UpdateDoKnockback();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionNormal = collision.GetContact(0).normal;

        TakeDamageAndKnockBack(KnockbackDamage);
    }

    public void TakeDamageAndKnockBack(int dmg)
    {
        if (!isInKnockbackInvincibility)
        {
            TakeDamage(dmg);
        }

        InitiateKnockback();
    }

    private void InitiateKnockback()
    {
        knockbackTimer = 0f;

        if (pCont)
        {
            pCont.CanMove = false; // Remove control from the player
        }

        if (pShipMov)
        {
            pShipMov.InitiateKnockbackWiggle(); // Commence the jigglin'
        }

        rb.velocity = collisionNormal * KnockbackSpeed; // Set velocity to the normal of collision times KnockbackSpeed

        isInKnockback = true; // Let update do its thing with this bool

        isInKnockbackInvincibility = true;
    }

    private void UpdateDoKnockback()
    {
        rb.velocity *= knockbackSlowingRate;

        knockbackTimer += Time.deltaTime;

        if (knockbackTimer >= knockbackTime) // if knockbackTime seconds have passed
        {
            EndKnockback();
        }
    }

    private void EndKnockback()
    {
        knockbackTimer = 0f;

        isInKnockback = false;

        isInKnockbackInvincibility = false;

        if (pCont)
        {
            pCont.CanMove = true; // Restore control
        }

        if (pShipMov)
        {
            pShipMov.EndKnockbackWiggle(); // Stop the ship from wiggling
        }
    }

    // Take damage. Should be called when hit with a projectile, etc.
    public void TakeDamage(int dmg)
    {
        if (shieldBar.CheckIfShieldDelpeted())
        {
            healthBar.Damage(dmg);
        }
        else
        {
            shieldBar.DamageShield(dmg);
        }
    }
}
