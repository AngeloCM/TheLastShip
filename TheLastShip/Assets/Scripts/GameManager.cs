using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

/// <summary>
/// This script controls the scenes, save data, and checkpoints.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SaveTrigger;

    [SerializeField]
    private GameObject ZeroTrigger; //Triggers the beginning of the gameplay loop.

    [SerializeField]
    private GameObject FirstTrigger; //Triggers gameplay event. 
    [SerializeField]
    private GameObject SecondTrigger; //Triggers gameplay event. 
    [SerializeField]
    private GameObject ThirdTrigger; //Triggers gameplay event. 

    [SerializeField]
    private GameObject BossTrigger; //Triggers boss to appear.

    private GameObject cargoShip;
    private GameObject playerShip;


    //Select a scene to load by typing in their index number.
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void Awake()
    {
        PositionShips();
    }

    private void PositionShips()
    {
        cargoShip = GameObject.FindGameObjectWithTag("CargoShip");
        playerShip = GameObject.FindGameObjectWithTag("Player");

        cargoShip.transform.position = SaveSystem.CargoShipPosition;
        playerShip.transform.position = SaveSystem.PlayerShipPosition;
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
}