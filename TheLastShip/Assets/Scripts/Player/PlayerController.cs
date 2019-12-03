using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The top speed at which the player can travel forward."), Range(5f, 1000f)]
    public float TopSpeed = 100f;

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

    private float currentSpeed;

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

        AccelerateShip();

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            shotTimer = 0f;
        }
    }

    private void RotateShip()
    {
        yaw = Input.GetAxis("Horizontal") * Time.deltaTime * RotationSpeed;
        pitch = Input.GetAxis("Vertical") * Time.deltaTime * RotationSpeed;
        roll = -Input.GetAxis("Triggers") * Time.deltaTime * RotationSpeed;

        this.transform.Rotate(new Vector3(0f, yaw, 0f));
        this.transform.Rotate(new Vector3(pitch, 0f, 0f));
        this.transform.Rotate(new Vector3(0f, 0f, roll));
    }

    private void AccelerateShip()
    {
        if (Input.GetButton("Throttle"))
        {
            if (currentSpeed < TopSpeed)
            {
                currentSpeed += AccelerationValue * Time.deltaTime;

                if (currentSpeed > TopSpeed) currentSpeed = TopSpeed;
            }
        }

        if (Input.GetButton("Brake"))
        {
            if (currentSpeed > 0f)
            {
                currentSpeed -= AccelerationValue * Time.deltaTime;

                if (currentSpeed < 0f) currentSpeed = 0f;
            }
        }
        
        rb.velocity = transform.forward * currentSpeed;
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
