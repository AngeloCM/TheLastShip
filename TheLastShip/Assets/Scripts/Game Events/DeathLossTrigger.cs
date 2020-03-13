using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLossTrigger : MonoBehaviour
{
    public void Die()
    {
        this.gameObject.GetComponent<ExplosionPlayer>().CreateExplosion();

        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        this.gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;

        StartCoroutine("ReloadSceneAfterSecond");
    }

    IEnumerator ReloadSceneAfterSecond()
    {
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Will reload the current scene regardless of what it is
    }
}
