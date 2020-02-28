using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairAlign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AlignCrosshair();
    }

    private void AlignCrosshair()
    {
        float crosshairHeight = Camera.main.scaledPixelHeight * 0.14f;

        // Align via a point [arbitrary large number] units straight forward from the camera
        this.GetComponent<RectTransform>().localPosition = new Vector3(0f, crosshairHeight, 0f);
    }
}
