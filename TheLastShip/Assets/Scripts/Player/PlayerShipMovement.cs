using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the movement of the player ship in reaction to actions taken by the player.
/// Actions include movement based on turning and thrust.
/// </summary>
public class PlayerShipMovement : MonoBehaviour
{
    [SerializeField, Tooltip("The player ship model.")]
    public GameObject PlayerShipModel;

    private PlayerController pCont;

    [HideInInspector]
    public Vector3 PlayerShipTargetLocation;

    private Vector3 prevRotation; // The rotation of the ship on the previous frame

    // Start is called before the first frame update
    void Start()
    {
        pCont = this.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, PlayerShipTargetLocation, 0.4f);

        if (this.gameObject.transform.rotation.eulerAngles.y < prevRotation.y)
        {

        }

        // Set previous rotation at the end of the current step.
        prevRotation = this.gameObject.transform.rotation.eulerAngles;
    }
}
