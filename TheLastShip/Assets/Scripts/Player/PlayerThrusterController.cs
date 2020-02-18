using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrusterController : MonoBehaviour
{
    private Rigidbody playerRb;

    private PlayerController pCont;

    private ParticleSystem pSys;

    [SerializeField, Tooltip("The maximum speed at which the thrusters emit particles.")]
    private float maximumEmissionSpeed;

    [SerializeField, Tooltip("The minimum speed at which the thrusters emit particles.")]
    private float minimumEmissionSpeed;

    [SerializeField, Tooltip("The maximum size at which the thrusters emit particles.")]
    private float maximumEmissionSize;

    [SerializeField, Tooltip("The minimum size at which the thrusters emit particles.")]
    private float minimumEmissionSize;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.transform.root.gameObject.GetComponent<Rigidbody>();

        pCont = this.transform.root.gameObject.GetComponent<PlayerController>();

        pSys = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedBasedOnRootSpeed();
    }

    private void UpdateSpeedBasedOnRootSpeed()
    {
        ParticleSystem.EmissionModule emission = pSys.emission;
        ParticleSystem.MainModule main = pSys.main;

        if (pCont.CurrentSpeed - pCont.MinSpeed < (pCont.TopSpeed - pCont.MinSpeed) * 0.01)
        {
            emission.enabled = false;
        }
        else
        {
            emission.enabled = true;
        }

        // Adjust the current emission speed relative to the root GameObject's speed
        main.startSpeed = minimumEmissionSpeed + (pCont.CurrentSpeed / pCont.TopSpeed) * (maximumEmissionSpeed - minimumEmissionSpeed);

        // Adjust the current emission size the same way
        main.startSize = minimumEmissionSize + (pCont.CurrentSpeed / pCont.TopSpeed) * (maximumEmissionSize - minimumEmissionSize);
    }
}
