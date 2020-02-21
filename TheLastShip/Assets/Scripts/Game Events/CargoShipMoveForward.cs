using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShipMoveForward : MonoBehaviour
{
    [SerializeField]
    private float CargoShipSpeed;
    
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(0f, 0f, CargoShipSpeed);
    }
}
