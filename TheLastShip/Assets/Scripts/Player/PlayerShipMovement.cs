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

    [SerializeField, Tooltip("The camera GameObject.")]
    public GameObject CameraGameObject;

    [SerializeField, Tooltip("The position of the ship model when moving straight.")]
    private Vector3 modelPosStraight;
    [SerializeField, Tooltip("The position of the ship model when turning upwards.")]
    private Vector3 modelPosBottom;
    [SerializeField, Tooltip("The position of the ship model when turning downwards.")]
    private Vector3 modelPosTop;
    [SerializeField, Tooltip("The position of the ship model when turning right.")]
    private Vector3 modelPosLeft;
    [SerializeField, Tooltip("The position of the ship model when turning left.")]
    private Vector3 modelPosRight;

    [SerializeField, Tooltip("The position of the camera when maintaining a constant speed.")]
    private Vector3 cameraPosCoasting;
    [SerializeField, Tooltip("The position of the camera when accelerating.")]
    private Vector3 cameraPosAccelerating;
    [SerializeField, Tooltip("The position of the camera when decelerating.")]
    private Vector3 cameraPosDecelerating;

    private PlayerController pCont;

    private PlayerController.TurnDirection currentTurnDir;

    private PlayerController.AccelerationState currentAccel;

    private Vector3 PlayerShipTargetLocation;

    private Vector3 CameraTargetLocation;

    // Start is called before the first frame update
    void Start()
    {
        pCont = this.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTurnDir = pCont.CurrentTurnDirection; // Update turn direction

        currentAccel = pCont.CurrentAcceleration; // Update acceleration

        UpdatePlayerShipLocation();

        UpdateCameraLocation();
    }

    private void UpdatePlayerShipLocation()
    {
        if (!GameSettings.IsPaused)
            PlayerShipModel.transform.localPosition = Vector3.Slerp(PlayerShipModel.transform.localPosition, PlayerShipTargetLocation, 0.02f);

        switch (currentTurnDir)
        {
            case PlayerController.TurnDirection.down:
                PlayerShipTargetLocation = modelPosTop;
                break;
            case PlayerController.TurnDirection.up:
                PlayerShipTargetLocation = modelPosBottom;
                break;
            case PlayerController.TurnDirection.right:
                PlayerShipTargetLocation = modelPosLeft;
                break;
            case PlayerController.TurnDirection.left:
                PlayerShipTargetLocation = modelPosRight;
                break;
            case PlayerController.TurnDirection.none:
                PlayerShipTargetLocation = modelPosStraight;
                break;
        }
    }

    private void UpdateCameraLocation()
    {
        if (!GameSettings.IsPaused)
            CameraGameObject.transform.localPosition = Vector3.Lerp(CameraGameObject.transform.localPosition, CameraTargetLocation, 0.08f);

        switch (currentAccel)
        {
            case PlayerController.AccelerationState.accelerating:
                CameraTargetLocation = cameraPosAccelerating;
                break;
            case PlayerController.AccelerationState.coasting:
                CameraTargetLocation = cameraPosCoasting;
                break;
            case PlayerController.AccelerationState.decelerating:
                CameraTargetLocation = cameraPosDecelerating;
                break;
        }
    }
}
