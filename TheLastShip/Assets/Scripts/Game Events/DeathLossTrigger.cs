using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLossTrigger : MonoBehaviour
{
    public void Die()
    {
        // TODO: Make explosion animation happen.
        // Wait to reload scene, and use checkpoint data to reload at checkpoint.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Will reload the current scene regardless of what it is
    }
}
