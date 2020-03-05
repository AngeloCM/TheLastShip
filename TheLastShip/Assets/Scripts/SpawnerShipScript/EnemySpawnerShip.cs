using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerShip : MonoBehaviour
{
    public enum EnableState { Enabled, Disabled}
    public float DistanceFromShipNeededToSpawn;
    public GameObject CargoShipReference;
    EnableState state;
    private List<GameObject> childList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        DisableAllChildSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
        float checkDisatanceFromCargoShip = Vector3.Distance(this.transform.position, CargoShipReference.transform.position);

        if(checkDisatanceFromCargoShip <= DistanceFromShipNeededToSpawn && this.state == EnableState.Disabled)
        {
            EnableAllChildSpawners();
        }
        if(checkDisatanceFromCargoShip > DistanceFromShipNeededToSpawn && this.state == EnableState.Enabled)
        {
            DisableAllChildSpawners();
        }
    }

    public void EnableAllChildSpawners()
    {
        foreach(Transform child in this.transform.GetComponentsInChildren<Transform>())
        {
            child.GetComponentInChildren<EnemySpawnBehavior>().enabled = true;
        }
        this.state = EnableState.Enabled;
    }

    public void DisableAllChildSpawners()
    {
        foreach(Transform child in this.transform.GetComponentsInChildren<Transform>())
        {
            child.GetComponentInChildren<EnemySpawnBehavior>().enabled = false;
        }
        this.state = EnableState.Disabled;
    }
    
}
