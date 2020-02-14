using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryShot : MonoBehaviour
{
    [SerializeField, Tooltip("The speed at which the shot travels. Ensure that this is greater than the player's TopSpeed."), Range(10f, 1000f)]
    public float ShotSpeed = 150f;

    [SerializeField, Tooltip("The time in seconds it takes for a shot to despawn automatically."), Range(5f, 20f)]
    public float DespawnTime = 8f;

    [SerializeField, Tooltip("The damage an uncharged shot deals to an enemy."), Range(1, 25)]
    public int UnchargedShotDamage = 10;

    [SerializeField, Tooltip("The damage a charged shot deals to an enemy."), Range(1, 100)]
    public int ChargedShotDamage = 40;
    
    [HideInInspector]
    public bool Fired; // Whether the shot has been fired.

    [HideInInspector]
    public bool FullyCharged; // Whether the shot is fully charged.

    [HideInInspector]
    public bool ReadyToFire; // Whether the shot can be fired. Set to true when the shot has been charged to at least 1/3 of full charge.

    private Vector3 shotDir;

    private Rigidbody rb;

    private float timeActive;

    // Start is called before the first frame update
    void Start()
    {
        this.Fired = false;
        this.ReadyToFire = false;
        this.FullyCharged = false;

        rb = this.GetComponent<Rigidbody>();
        
        shotDir = this.transform.forward;

        timeActive = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fired && ReadyToFire)
        {
            // Detach this gameobject from the ShotTransform
            this.gameObject.transform.parent = null;

            shotDir = this.transform.forward;

            rb.velocity = shotDir * ShotSpeed;

            // Keep track of the time this shot has been active in the scene.
            timeActive += Time.deltaTime;

            // Deactivate this shot if it has been around for DespawnTime seconds.
            if (timeActive >= DespawnTime)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (Fired)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            // If not yet fired, keep the projectile on the transform parent (ShotTransform)
            this.gameObject.transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Fired)
        {
            // Destroy an enemy that has been collided with. Will change once enemies have health.
            if (other.transform.root.tag == "Enemy" || other.transform.root.tag == "enemy")
            {
                if (FullyCharged)
                {
                    other.transform.root.GetComponent<EnemyHealth>().TakeDamage(ChargedShotDamage);
                }
                else
                {
                    other.transform.root.GetComponent<EnemyHealth>().TakeDamage(UnchargedShotDamage);
                }

                this.gameObject.SetActive(false);
            }
        }
    }
}
