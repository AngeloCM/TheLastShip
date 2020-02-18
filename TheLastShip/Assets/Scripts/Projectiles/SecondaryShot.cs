using System;
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

    [SerializeField, Tooltip("The maximum scale of the shot when it expands upon hitting an enemy."), Range(2f, 100f)]
    public float ChargedShotExplodeScale;

    [SerializeField, Tooltip("The strength with which the shot homes an enemy. Anything above 0.5 should be considered an instant home."), Range(0f, 1f)]
    private float homingStrength = 0.1f;

    [HideInInspector]
    public bool Fired; // Whether the shot has been fired.

    [HideInInspector]
    public bool FullyCharged; // Whether the shot is fully charged.

    [HideInInspector]
    public bool ReadyToFire; // Whether the shot can be fired. Set to true when the shot has been charged to at least 1/3 of full charge.

    private Vector3 shotDir;

    private Rigidbody rb;

    private float timeActive;

    [HideInInspector]
    public GameObject TargetEnemy; // The target enemy to move towards when homing.

    private List<GameObject> enemiesDamaged;

    private bool hasCollided;
    private bool isExpanding;

    private float expansionRate = 1.1f; // The rate at which a fully charged shot expands on contact with first enemy. Ensure this is > 1

    private float postExpandLingerTime = 0.7f; // The time in seconds to linger when finished expanding
    private float lingerTimer; // The active timer keeping track of linger time

    private float distanceFromEnemyLastFrame; // These two variables determine whether to stop homing because the shot is "past" the enemy
    private float currentDistanceFromEnemy;

    // Start is called before the first frame update
    void Start()
    {
        this.Fired = false;
        this.ReadyToFire = false;
        this.FullyCharged = false;

        rb = this.GetComponent<Rigidbody>();

        enemiesDamaged = new List<GameObject>();
        
        shotDir = this.transform.forward;

        timeActive = 0f;

        this.hasCollided = false;
        this.isExpanding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fired && ReadyToFire)
        {
            // Detach this gameobject from the ShotTransform
            this.gameObject.transform.parent = null;
            
            // Update the shot direction to follow TargetEnemy if it exists and if the shot is fully charged
            if (TargetEnemy != null && FullyCharged)
            {
                currentDistanceFromEnemy = Vector3.Distance(this.transform.position, TargetEnemy.transform.position);

                Vector3 directionOfEnemy = Vector3.Normalize(TargetEnemy.transform.position - this.transform.position);

                if (currentDistanceFromEnemy < distanceFromEnemyLastFrame)
                {
                    shotDir = Vector3.RotateTowards(shotDir, Vector3.Lerp(shotDir, directionOfEnemy, homingStrength * 0.5f), Vector3.Angle(shotDir, directionOfEnemy), 1f);
                }

                // Ensure this happens after all comparisons between the two vectors are made.
                distanceFromEnemyLastFrame = currentDistanceFromEnemy;
            }


            // Move forward unless the shot has collided with an enemy, in which case we want the shot to sit still and expand.
            if (!hasCollided) rb.velocity = shotDir * ShotSpeed;
            else rb.velocity = Vector3.zero;

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
            // Delete if not yet ready to fire, but player fired
            this.gameObject.SetActive(false);
        }
        else
        {
            // If not yet fired, keep the projectile on the transform parent (ShotTransform)
            this.gameObject.transform.localPosition = Vector3.zero;

            // Also, keep shotDir as forward so the shot goes forward at first
            shotDir = this.transform.forward;

            // Update target enemy via a forward Raycast while this shot is active but not fired
            RaycastHit hit;

            if (TargetEnemy == null)
            {
                if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, 500f))
                {
                    if (hit.transform.root.gameObject.tag == "Enemy" || hit.transform.root.gameObject.tag == "enemy")
                    {
                        this.TargetEnemy = hit.transform.root.gameObject;
                    }
                }
            }
        }

        // Expand the scale of this shot if it's set to expand, until it hits the ChargedShotExplodeScale set on the prefab
        if (isExpanding && this.gameObject.transform.localScale.y < ChargedShotExplodeScale)
        {
            this.gameObject.transform.localScale *= expansionRate;
        }

        // Stop expanding once at ChargeShotExplodeScale, then disappear after postExpandLingerTime seconds
        if (this.gameObject.transform.localScale.y >= ChargedShotExplodeScale)
        {
            isExpanding = false;

            lingerTimer += Time.deltaTime;

            if (lingerTimer >= postExpandLingerTime)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Fired)
        {
            // Deal damage to an enemy collided with. The rest depends on whether the projectile has already hit an enemy.
            if (other.transform.root.tag == "Enemy" || other.transform.root.tag == "enemy")
            {
                if (!enemiesDamaged.Contains(other.transform.root.gameObject)) // Make sure no enemy is damaged more than once using enemiesDamaged list
                {

                    if (FullyCharged)
                    {
                        other.transform.root.GetComponent<EnemyHealth>().TakeDamage(ChargedShotDamage);
                    }
                    else
                    {
                        other.transform.root.GetComponent<EnemyHealth>().TakeDamage(UnchargedShotDamage);
                    }

                    enemiesDamaged.Add(other.transform.root.gameObject); // Add currently colliding enemy to list

                    if (FullyCharged && !hasCollided) // Begin expansion if this shot is fully charged and has not collided with anything yet
                    {
                        BeginExpand();
                    }
                    else // Just delete the shot if it's not fully charged
                    {
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    // Set a few variables so that Update knows to expand the shot
    private void BeginExpand()
    {
        hasCollided = true;

        isExpanding = true;
    }
}
