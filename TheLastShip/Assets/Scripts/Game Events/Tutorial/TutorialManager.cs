using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField, Tooltip("The number of enemies required to kill to spawn the cargo ship.")]
    private int requiredKills;

    [SerializeField, Tooltip("How far the player must be from the cargo ship to end the game.")]
    private float requiredDistanceFromCargo;

    [SerializeField, Tooltip("The (inactive) cargo ship GameObject in-scene to enable on kill condition satisfaction.")]
    private GameObject inactiveCargoGameObject;

    [SerializeField, Tooltip("The objective text in the HUD.")]
    private TextMeshProUGUI objectiveText;

    [HideInInspector]
    public int AccumulatedKills;

    private GameObject playerRef;

    private bool cargoSpawned;

    // Start is called before the first frame update
    void Start()
    {
        AccumulatedKills = 0;

        playerRef = GameObject.FindGameObjectWithTag("Player");

        objectiveText.text = "Objective: Drive Back the Swarm";
    }

    // Update is called once per frame
    void Update()
    {
        if (AccumulatedKills >= requiredKills && !cargoSpawned)
        {
            inactiveCargoGameObject.SetActive(true);

            objectiveText.text = "Objective: Rendezvous with the Cargo Vessel";

            cargoSpawned = true;
        }

        if (cargoSpawned)
        {
            if (Vector3.Distance(playerRef.transform.position, inactiveCargoGameObject.transform.position) < requiredDistanceFromCargo)
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}
