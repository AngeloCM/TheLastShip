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

    [SerializeField, Tooltip("The prefab for the player's basic projectile.")]
    public GameObject PlayerShotPrefab;

    [SerializeField, Tooltip("The transform at which to spawn a player shot.")]
    public Transform PlayerShotTransform;

    private Rigidbody rb;

    private float yaw, pitch, roll;

    public float CurrentSpeed { get; private set; }

    private float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        shotTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        RotateShip();

        if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.classic)
        {
            AccelerateShipClassic();

            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                shotTimer = 0f;
            }
        }

        else if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.frontline)
        {
            AccelerateShipFrontline();

            if (Input.GetButton("FireFrontline"))
            {
                Shoot();
            }
            if (Input.GetButtonUp("FireFrontline"))
            {
                shotTimer = 0f;
            }
        }

        else if (GameSettings.CurrentControlScheme == GameSettings.ControlScheme.frontlineBeta)
        {
            AccelerateShipFrontlineBeta();

            if (Input.GetButton("FireFrontline")) // Fire button is the same as frontline
            {
                Shoot();
            }
            if (Input.GetButtonUp("FireFrontline"))
            {
                shotTimer = 0f;
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
    }

    private void AccelerateShipClassic()
    {
        if (Input.GetButton("ThrottleClassic"))
        {
            if (CurrentSpeed < TopSpeed)
            {
                CurrentSpeed += AccelerationValue * Time.deltaTime;

                if (CurrentSpeed > TopSpeed) CurrentSpeed = TopSpeed;
            }
        }

        if (Input.GetButton("BrakeClassic"))
        {
            if (CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationValue * Time.deltaTime;

                if (CurrentSpeed < MinSpeed) CurrentSpeed = MinSpeed;
            }
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
            }
        }

        if (Input.GetAxisRaw("ThrottleFrontline") > 0.5f)
        {
            if (CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationValue * Time.deltaTime;

                if (CurrentSpeed < MinSpeed) CurrentSpeed = MinSpeed;
            }
        }

        //Debug.Log(Input.GetAxisRaw("ThrottleFrontline"));

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
            }
        }

        if (Input.GetAxisRaw("RollClassic") < -0.5f)
        {
            if (CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationValue * Time.deltaTime;

                if (CurrentSpeed < MinSpeed) CurrentSpeed = MinSpeed;
            }
        }

        rb.velocity = transform.forward * CurrentSpeed;
    }

    private void Shoot()
    {
        if (shotTimer <= 0f)
        {
            Instantiate(PlayerShotPrefab, PlayerShotTransform);

            shotTimer = ShotCooldown;
        }

        shotTimer -= Time.deltaTime;
    }
}
