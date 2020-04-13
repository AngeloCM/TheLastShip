using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // call appropriate music for the scene
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            PlayMusicMenu();
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
        {
            PlayMusicGameplay();
        }
    }

    private void PlayMusicMenu()
    {
        AkSoundEngine.PostEvent("mu_menu", gameObject);
    }

    private void PlayMusicGameplay()
    {
        AkSoundEngine.PostEvent("mu_gameplay", gameObject);
    }
}
