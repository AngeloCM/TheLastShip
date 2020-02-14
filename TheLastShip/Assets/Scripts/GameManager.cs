using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// This script controls the scenes, save data, and checkpoints.
/// </summary>
[System.Serializable]
public class GameManager : MonoBehaviour
{
    public int health;
    public float[] Playerposition;
    public float[] Cargoposition;
   
    //Load next scene based on index position in the buildsettings tab.
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    //Forces the game to close.
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Application Closed");
    }
   //Saves position of players and cargoship based on position of the last checkpoint reached.
    public void GameplayData(PlayerController player)
    {
        // health = HealthSlider.Maxhealth;

        Playerposition = new float[3];
        Playerposition[0] = player.transform.position.x;
        Playerposition[1] = player.transform.position.y;
        Playerposition[2] = player.transform.position.z;

       

    }
}
