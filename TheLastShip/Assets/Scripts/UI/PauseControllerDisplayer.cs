using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseControllerDisplayer : MonoBehaviour
{
    [SerializeField]
    private Sprite classicController, alphaController, betaController;

    [SerializeField]
    private Button classicBut, alphaBut, betaBut;

    private Image thisImage;

    // Start is called before the first frame update
    void Start()
    {
        thisImage = this.gameObject.GetComponent<Image>();

        thisImage.sprite = betaController;
    }

    // Update is called once per frame
    void Update()
    {
        if (classicBut.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            thisImage.sprite = classicController;
        }

        if (alphaBut.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            thisImage.sprite = alphaController;
        }

        if (betaBut.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            thisImage.sprite = betaController;
        }
    }
}
