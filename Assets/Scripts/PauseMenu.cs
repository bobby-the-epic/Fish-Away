using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    AudioSource backgroundMusic;
    public GameObject pauseMenu, optionsMenu;
    public Slider volumeSlider;

    public void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.gameOver)
        {
            if (!pauseMenu.activeInHierarchy && !optionsMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (optionsMenu.activeInHierarchy)
            {
                GoBack();
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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void VolumeAdjust()
    {
        backgroundMusic.volume = volumeSlider.value;
    }
}
