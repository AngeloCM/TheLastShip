using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlastZoneTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "player")
        {
            // TODO: Make explosion animation happen, as well as maybe 5 sec-ish countdown to explosion.
            // Wait to reload scene, and use checkpoint data to reload at checkpoint.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Will reload the current scene regardless of what it is
        }
    }
}
