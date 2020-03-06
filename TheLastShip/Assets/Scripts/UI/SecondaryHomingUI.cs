using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryHomingUI : MonoBehaviour
{
    private int pixH;

    private RectTransform rect;

    private Camera cam;

    private GameObject targetEnemy;

    [HideInInspector]
    public SecondaryShot CurrentShot;

    // Start is called before the first frame update
    void Start()
    {
        pixH = Camera.main.scaledPixelHeight;
        rect = this.GetComponent<RectTransform>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        AttemptRetrieveTargetEnemy();
    }

    private void AttemptRetrieveTargetEnemy()
    {
        // Make sure the shot is ready to home and not fired
        if (CurrentShot != null && CurrentShot.isActiveAndEnabled && !CurrentShot.Fired)
        {
            // Get the shot's target enemy
            targetEnemy = CurrentShot.TargetEnemy;

            if (targetEnemy != null && targetEnemy.activeInHierarchy)
            {
                // Place the crosshair on the enemy
                PlaceCrosshairOnEnemy();
            }
        }
        else
        {
            // If there is no ready shot, go back to original spot
            CurrentShot = null;

            AlignCrosshair();
        }
    }

    private void PlaceCrosshairOnEnemy()
    {
        this.gameObject.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(targetEnemy.transform.position);
    }

    private void AlignCrosshair()
    {
        float crosshairHeight = pixH * 0.14f;

        // Align via a point [arbitrary large number] units straight forward from the camera
        rect.localPosition = new Vector3(0f, crosshairHeight, 0f);
    }
}
