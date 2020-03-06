using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSelectableBehavior : MonoBehaviour
{
    [SerializeField, Tooltip("The text to display on the HUDwhen the player nears this object.")]
    private string objectiveTextToDisplay;

    [SerializeField, Tooltip("The objective text game object on the main HUD.")]
    private GameObject objectiveText;

    [SerializeField, Tooltip("The name of the scene this planet should load.")]
    public string SceneToLoad; 

    private TextMeshProUGUI objectiveTextTM;

    private GameObject parentPlanet; // The planet this script's game object is childed to.

    [HideInInspector]
    public bool CanLoadScene; // Whether the player can load a scene by shooting the planet.

    // Start is called before the first frame update
    void Start()
    {
        objectiveTextTM = objectiveText.GetComponent<TextMeshProUGUI>();

        parentPlanet = this.transform.parent.gameObject;

        CanLoadScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "player")
        {
            objectiveTextTM.text = objectiveTextToDisplay;

            CanLoadScene = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "player")
        {
            objectiveTextTM.text = "Objective: Approach a Planet";

            CanLoadScene = false;
        }
    }
}
