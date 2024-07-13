using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu;
    public Button resumeButton;
    public Button optionsButton;
    public Button quitButton;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ShowOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void GoBack()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
