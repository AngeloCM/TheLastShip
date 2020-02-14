using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZoneDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject blastZone;

    bool hasWarned = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "player")
        {
            if (!hasWarned)
            {
                // TODO: Pending voice acting, put "return to the ship!" voice line trigger here
                Debug.Log("Voice line: Return to the ship!");

                hasWarned = true;

                blastZone.GetComponent<MeshRenderer>().enabled = true; // Make blast zone visible
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "player")
        {
            if (hasWarned)
            {
                hasWarned = false;

                blastZone.GetComponent<MeshRenderer>().enabled = false; // Make blast zone invisible
            }
        }
    }
}
