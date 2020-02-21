using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUpdater : MonoBehaviour
{
    [SerializeField]
    private Vector3 CargoShipPosition;
    [SerializeField]
    private Vector3 PlayerShipPosition;

    [SerializeField]
    private BarManager CargoShipHealth;
    [SerializeField]
    private BarManager PlayerShipHealth;

    [SerializeField]
    private int CheckPointNumber;

    private void OnTriggerEnter(Collider collider )
    {
        if (collider.gameObject.tag == "CargoShip")
        {
            SaveSystem.UpdateCheckPoint(CargoShipPosition, PlayerShipPosition, CargoShipHealth.GetCurrentValue(), PlayerShipHealth.GetCurrentValue(), CheckPointNumber);
        }
    }
}
