using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject optionsScreen, titleScreen;

    public void Start()
    {
        volumeSlider.value = GameManager.volume;
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && optionsScreen.activeInHierarchy)
        {
            GoBack();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("FishingGame");
    }
    public void VolumeAdjust()
    {
        GameManager.volume = volumeSlider.value;
    }
    public void OptionsMenu()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }
    public void GoBack()
    {
        optionsScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
}
