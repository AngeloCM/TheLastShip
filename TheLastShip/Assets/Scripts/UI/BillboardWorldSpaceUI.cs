using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardWorldSpaceUI : MonoBehaviour
{
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(this.transform.position - cameraTransform.position, Vector3.up);
    }
}
