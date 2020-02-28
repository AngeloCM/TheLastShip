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
    //[SerializeField]
    //private GameObject SaveTrigger;

    //[SerializeField]
    //private GameObject ZeroTrigger; //Triggers the beginning of the gameplay loop.

    //[SerializeField]
    //private GameObject FirstTrigger; //Triggers gameplay event. 
    //[SerializeField]
    //private GameObject SecondTrigger; //Triggers gameplay event. 
    //[SerializeField]
    //private GameObject ThirdTrigger; //Triggers gameplay event. 

    //[SerializeField]
    //private GameObject BossTrigger; //Triggers boss to appear.

    [SerializeField, Tooltip("The prefab for an enemy spawner to be instantiated throughout the game.")]
    private GameObject enemySpawnerPrefab;

    private int sequenceIndex; // The index of the sequence to play. Causes a sequence to start either on scene load or when != prevSequenceIndex.
    private int prevSequenceIndex = -1; // Lets us know when to start a new sequence. Default at -1 so it always happens on scene load.

    private float sequenceTime; // Keeps track of the time the current sequence has gone on for. Allows things like spawning an enemy ship 20 seconds into a particular sequence.
    private int eventsExecutedThisSequence; // Keeps track of the events executed so far in a sequence. Simple way of making sure each event only happens once.

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

        SetSequence();
    }

    private void Update()
    {
        sequenceTime += Time.deltaTime;

        // Continuously check if the sequence has changed
        SetSequence();

        switch (sequenceIndex)
        {
            case 0:
                ExecuteSequenceOneEvents();
                break;
            case 1:
                ExecuteSequenceTwoEvents();
                break;
            case 2:
                ExecuteSequenceThreeEvents();
                break;
            case 3:
                ExecuteBossSequenceEvents();
                break;
        }
    }

    // Here is where any events happening in Sequence 1 should be placed.
    private void ExecuteSequenceOneEvents()
    {
        // An example of spawning an enemy at a specific time (in this case, at 20 seconds):
        if (eventsExecutedThisSequence < 1 && sequenceTime >= 20f)
        {
            Instantiate(enemySpawnerPrefab, new Vector3(-640f, 0, 800f), Quaternion.Euler(0f, 90f, 0f));

            eventsExecutedThisSequence++;

            Debug.Log("Enemy wave 1 spawned");
        }

        if (eventsExecutedThisSequence < 2 && sequenceTime >= 40f)
        {
            Instantiate(enemySpawnerPrefab, new Vector3(640f, 0, 1600f), Quaternion.Euler(0f, -90f, 0f));

            eventsExecutedThisSequence++;

            Debug.Log("Enemy wave 2 spawned");
        }
    }

    // Here is where any events happening in Sequence 2 should be placed.
    private void ExecuteSequenceTwoEvents()
    {

    }

    // Here is where any events happening in Sequence 3 should be placed.
    private void ExecuteSequenceThreeEvents()
    {

    }

    // Here is where any events happening in the boss sequence should be placed.
    private void ExecuteBossSequenceEvents()
    {

    }

    private void SetSequence()
    {
        // Get the new sequenceIndex
        sequenceIndex = SaveSystem.CheckPointNumber;

        if (sequenceIndex != prevSequenceIndex)
        {
            // Reset sequence time
            sequenceTime = 0f;

            // Reset this so we can use it again in the next/current sequence
            eventsExecutedThisSequence = 0;

            prevSequenceIndex = sequenceIndex;
        }
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