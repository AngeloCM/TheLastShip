using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
    [SerializeField, Tooltip("The speed at which the shot travels. Ensure that this is greater than the player's TopSpeed."), Range(10f, 1000f)]
    public float ShotSpeed = 200f;

    [SerializeField, Tooltip("The time in seconds it takes for a shot to despawn automatically."), Range(5f, 20f)]
    public float DespawnTime = 5f;

    private Vector3 shotDir;

    private Rigidbody rb;

    private float timeActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        // This is transform.up because the shot is rotated 90 degrees on X axis. Therefore, subject to change.
        shotDir = this.transform.up;

        timeActive = 0f;

        // Detach this gameobject from the ShotTransform
        this.gameObject.transform.parent = null; 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = shotDir * ShotSpeed;

        // Keep track of the time this shot has been active in the scene.
        timeActive += Time.deltaTime;

        // Deactivate this shot if it has been around for DespawnTime seconds.
        if (timeActive >= DespawnTime)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy an enemy that has been collided with.
        if (other.tag == "Enemy" || other.tag == "enemy")
        {
            // TODO: Lessen enemy's health and only destroy if health <= 0
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
