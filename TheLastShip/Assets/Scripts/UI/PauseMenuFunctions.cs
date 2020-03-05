using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuFunctions : MonoBehaviour
{
    [SerializeField]
    private Button frontlineBetaButton;

    [SerializeField]
    private Toggle invertYLookToggle;

    private PlayerController pCont;

    private void Awake()
    {
        pCont = FindObjectOfType<PlayerController>();
        pCont.CanMove = false;

        Time.timeScale = 0f;

        GameSettings.IsPaused = true;

        frontlineBetaButton.Select();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause"))
        {
            pCont.CanMove = true;

            Time.timeScale = 1f;

            GameSettings.IsPaused = false;

            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        pCont.CanMove = false;

        Time.timeScale = 0f;

        GameSettings.IsPaused = true;

        frontlineBetaButton.Select();
    }

    public void SetControlScheme(string scheme) // must use a string here in order to plug into Unity default OnClick function
    {
        if (scheme == "classic") GameSettings.CurrentControlScheme = GameSettings.ControlScheme.classic;
        if (scheme == "frontline") GameSettings.CurrentControlScheme = GameSettings.ControlScheme.frontline;
        if (scheme == "frontlineBeta") GameSettings.CurrentControlScheme = GameSettings.ControlScheme.frontlineBeta;
    }

    public void SetInvertYLook()
    {
        GameSettings.InvertYLook = invertYLookToggle.isOn;
    }
}
