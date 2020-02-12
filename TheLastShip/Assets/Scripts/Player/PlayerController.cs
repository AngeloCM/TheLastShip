using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The top speed at which the player can travel forward."), Range(5f, 1000f)]
    public float TopSpeed = 100f;

    [SerializeField, Tooltip("The minimum speed at which the player can move."), Range(0f, 1000f)]
    public float MinSpeed = 100f;

    [SerializeField, Tooltip("The speed at which the player's ship rotates."), Range(1f, 500f)]
    public float RotationSpeed = 100f;

    [SerializeField, Tooltip("The speed at which the player's ship accelerates."), Range(0.1f, 100f)]
    public float AccelerationValue = 50f;

    [SerializeField, Tooltip("The cooldown in seconds between basic shots."), Range(0.01f, 2f)]
    public float ShotCooldown = 0.3f;

    [SerializeField, Tooltip("The time in seconds it takes to charge a secondary (charge) shot."), Range(0.01f, 2f)]
    public float SecondaryChargeTime = 1f;

    [SerializeField, Tooltip("The prefab for the player's basic projectile.")]
    public GameObject PlayerShotPrefab;

    [SerializeField, Tooltip("The prefab for the player's secondary (charge) projectile.")]
    public GameObject PlayerSecondaryShotPrefab;

    [SerializeField, Tooltip("The transform at which to spawn a player shot.")]
    public Transform PlayerShotTransform;

    public bool CanMove; // Whether the player can move the ship. Assign with caution.

    private Rigidbody rb;

    private float yaw, pitch, roll;

    public enum TurnDirection // An enum for the direction the ship is currently turning
    {
        up, down, right, left, none
    }

    [HideInInspector]
    public TurnDirection CurrentTurnDirection; // Keeps track of current turn direction. Used by PlayerShipMovement.cs

    public float CurrentSpeed { get; private set; } // Current speed the player is traveling at.

    public enum AccelerationState
    {
        accelerating, coasting, decelerating
    }

    [HideInInspector]
    public AccelerationState CurrentAcceleration;

    private float shotTimer;

    private float chargeTimer; // The current charge time of the player's secondary shot.

    private GameObject currentChargeShot;

    private float chargeShotMinScale = 0.5f;
    private float chargeShotMaxScale = 2f;

    private bool firing; // Keeps track of whether player is firing, so they can only use one fire mode at a time.
    private bool firingPrimary; // Keeps track of whether player is firing primary fire, so it may continuously fire.
    private bool chargingSecondary; // Keeps track of whether player is firing secondary fire, so it may charge.

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        firing = false;
        firingPrimary = false;
        chargingSecondary = false;

        CanMove = true;

        CurrentTurnDirection = TurnDirection.none;
        CurrentAcceleration = AccelerationState.coasting;

        shotTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            RotateShip();

            if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.classic)
            {
                AccelerateShipClassic();

                if (Input.GetButton("Fire1") && !firing)
                {
                    firingPrimary = true;
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    firing = false;
                    firingPrimary = false;
                    shotTimer = 0f;
                }

                if (Input.GetButton("Fire2") && !firing)
                {
                    chargingSecondary = true;
                }
                if (Input.GetButtonUp("Fire2") && currentChargeShot != null)
                {
                    ReleaseSecondaryFire();

                    shotTimer = 0f;
                }
            }

            else if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.frontline)
            {
                AccelerateShipFrontline();

                if (Input.GetButton("FireFrontline") && !firing)
                {
                    firingPrimary = true;
                }
                if (Input.GetButtonUp("FireFrontline"))
                {
                    firing = false;
                    firingPrimary = false;
                    shotTimer = 0f;
                }

                if (Input.GetButton("AltFireFrontline") && !firing)
                {
                    chargingSecondary = true;
                }
                if (Input.GetButtonUp("AltFireFrontline") && currentChargeShot != null)
                {
                    ReleaseSecondaryFire();

                    shotTimer = 0f;
                }
            }

            else if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.frontlineBeta)
            {
                AccelerateShipFrontlineBeta();

                if (Input.GetButton("FireFrontline") && !firing) // Fire button is the same as frontline
                {
                    firingPrimary = true;
                }
                if (Input.GetButtonUp("FireFrontline"))
                {
                    firing = false;
                    firingPrimary = false;
                    shotTimer = 0f;
                }

                if (Input.GetButton("AltFireFrontline") && !firing)
                {
                    chargingSecondary = true;
                }
                if (Input.GetButtonUp("AltFireFrontline") && currentChargeShot != null)
                {
                    ReleaseSecondaryFire();

                    shotTimer = 0f;
                }
            }

            if (firingPrimary)
            {
                Shoot();
            }

            if (chargingSecondary)
            {
                ChargeSecondaryFire();
            }
        }
    }

    private void RotateShip()
    {
        if (GameSettings.InvertYLook)
        {
            pitch = Input.GetAxis("Vertical") * Time.deltaTime * RotationSpeed;
        }
        else
        {
            pitch = -Input.GetAxis("Vertical") * Time.deltaTime * RotationSpeed;
        }
        
        yaw = Input.GetAxis("Horizontal") * Time.deltaTime * RotationSpeed;

        if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.classic || GameSettings.CurrentControlScheme == GameSettings.ControlScheme.frontline)
        {
            roll = -Input.GetAxis("RollClassic") * Time.deltaTime * RotationSpeed; // Roll is the same between classic and frontline
        }
        else if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.frontlineBeta)
        {
            roll = -Input.GetAxis("RollFrontlineBeta") * Time.deltaTime * RotationSpeed;
        }

        this.transform.Rotate(new Vector3(0f, yaw, 0f));
        this.transform.Rotate(new Vector3(pitch, 0f, 0f));
        this.transform.Rotate(new Vector3(0f, 0f, roll));

        // Update the turn direction (for PlayerShipMovement)
        UpdateTurnDirection();
    }

    private void UpdateTurnDirection()
    {
        if (Input.GetAxis("Horizontal") > 0 && Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
        {
            CurrentTurnDirection = TurnDirection.right;
        }

        if (Input.GetAxis("Horizontal") < 0 && Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
        {
            CurrentTurnDirection = TurnDirection.left;
        }

        if (Input.GetAxis("Vertical") > 0 && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Horizontal")))
        {
            if (GameSettings.InvertYLook)
            {
                CurrentTurnDirection = TurnDirection.down;
            }
            else
            {
                CurrentTurnDirection = TurnDirection.up;
            }
        }

        if (Input.GetAxis("Vertical") < 0 && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Horizontal")))
        {
            if (GameSettings.InvertYLook)
            {
                CurrentTurnDirection = TurnDirection.up;
            }
            else
            {
                CurrentTurnDirection = TurnDirection.down;
            }
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            CurrentTurnDirection = TurnDirection.none;
        }
    }

    private void AccelerateShipClassic()
    {
        if (Input.GetButton("ThrottleClassic"))
        {
            if (CurrentSpeed < TopSpeed)
            {
                CurrentSpeed += AccelerationValue * Time.deltaTime;

                if (CurrentSpeed > TopSpeed) CurrentSpeed = TopSpeed;
                else CurrentAcceleration = AccelerationState.accelerating;
            }
        }

        if (Input.GetButton("BrakeClassic"))
        {
            if (CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationValue * Time.deltaTime;

                if (CurrentSpeed < MinSpeed) CurrentSpeed = MinSpeed;
                else CurrentAcceleration = AccelerationState.decelerating;
            }
        }

        if (!Input.GetButton("ThrottleClassic") && !Input.GetButton("BrakeClassic"))
        {
            CurrentAcceleration = AccelerationState.coasting;
        }
        
        rb.velocity = transform.forward * CurrentSpeed;
    }
    
    private void AccelerateShipFrontline()
    {
        if (Input.GetAxisRaw("ThrottleFrontline") < -0.5f)
        {
            if (CurrentSpeed < TopSpeed)
            {
                CurrentSpeed += AccelerationValue * Time.deltaTime;

                if (CurrentSpeed > TopSpeed) CurrentSpeed = TopSpeed;
                else CurrentAcceleration = AccelerationState.accelerating;
            }
        }

        if (Input.GetAxisRaw("ThrottleFrontline") > 0.5f)
        {
            if (CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationValue * Time.deltaTime;

                if (CurrentSpeed < MinSpeed) CurrentSpeed = MinSpeed;
                else CurrentAcceleration = AccelerationState.accelerating;
            }
        }

        if (Input.GetAxisRaw("ThrottleFrontline") < 0.5f && Input.GetAxisRaw("ThrottleFrontline") > -0.5f)
        {
            CurrentAcceleration = AccelerationState.coasting;
        }

            rb.velocity = transform.forward * CurrentSpeed;
    }
    
    private void AccelerateShipFrontlineBeta() // The RollClassic axis is the triggers, which will be used here for throttle instead.
    {
        if (Input.GetAxisRaw("RollClassic") > 0.5f)
        {
            if (CurrentSpeed < TopSpeed)
            {
                CurrentSpeed += AccelerationValue * Time.deltaTime;

                if (CurrentSpeed > TopSpeed) CurrentSpeed = TopSpeed;
                else CurrentAcceleration = AccelerationState.accelerating;
            }
        }

        if (Input.GetAxisRaw("RollClassic") < -0.5f)
        {
            if (CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationValue * Time.deltaTime;

                if (CurrentSpeed < MinSpeed) CurrentSpeed = MinSpeed;
                else CurrentAcceleration = AccelerationState.decelerating;
            }
        }

        if (Input.GetAxisRaw("RollClassic") < 0.5f && Input.GetAxisRaw("RollClassic") > -0.5f)
        {
            CurrentAcceleration = AccelerationState.coasting;
        }

        rb.velocity = transform.forward * CurrentSpeed;
    }

    private void Shoot()
    {
        firing = true;

        if (shotTimer <= 0f)
        {
            // Instantiate two shots on the left and right blasters.
            Instantiate(PlayerShotPrefab, PlayerShotTransform.Find("PlayerShotTransformLeft"));
            Instantiate(PlayerShotPrefab, PlayerShotTransform.Find("PlayerShotTransformRight"));

            shotTimer = ShotCooldown;
        }

        shotTimer -= Time.deltaTime;
    }

    private void ChargeSecondaryFire()
    {
        if (!firing)
        {
            currentChargeShot = Instantiate(PlayerSecondaryShotPrefab, PlayerShotTransform);
        }

        firing = true;

        // Scale the charge shot up gradually to indicate its progress towards full charge.
        float chargeShotCurrentScale = chargeShotMinScale + (chargeShotMaxScale - chargeShotMinScale) * (chargeTimer / SecondaryChargeTime);

        currentChargeShot.transform.localScale = new Vector3(chargeShotCurrentScale, chargeShotCurrentScale, chargeShotCurrentScale);

        if (chargeTimer >= SecondaryChargeTime / 3)
        {
            currentChargeShot.GetComponent<SecondaryShot>().ReadyToFire = true;
        }

        if (chargeTimer >= SecondaryChargeTime)
        {
            currentChargeShot.GetComponent<SecondaryShot>().FullyCharged = true;
        }
        else
        {
            chargeTimer += Time.deltaTime;
        }
    }

    private void ReleaseSecondaryFire()
    {
        firing = false;
        chargingSecondary = false;

        chargeTimer = 0f;

        currentChargeShot.GetComponent<SecondaryShot>().Fired = true;

        currentChargeShot = null; // Set to null so we can check if there is a shot currently being charged
    }
}
