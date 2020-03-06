using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSceneLoad : MonoBehaviour
{
    private MenuSelectableBehavior menuBehavior;

    private void Start()
    {
        menuBehavior = GetComponentInChildren<MenuSelectableBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShot" && menuBehavior.CanLoadScene)
        {
            if (Application.CanStreamedLevelBeLoaded(menuBehavior.SceneToLoad))
            {
                SceneManager.LoadScene(menuBehavior.SceneToLoad);
            }
            else if (menuBehavior.SceneToLoad == "Exit")
            {
                Application.Quit();

                Debug.Log("Application Quit");
            }
        }
    }
}
