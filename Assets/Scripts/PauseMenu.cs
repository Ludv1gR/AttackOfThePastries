using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject menu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        menu.SetActive(false);
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        menu.SetActive(true);
        Time.timeScale = 1f;

    }

    public void Update()
    {
        if (pauseMenu.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
}
