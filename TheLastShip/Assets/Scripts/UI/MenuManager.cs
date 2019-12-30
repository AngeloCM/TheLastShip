using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
        }
    }
}
