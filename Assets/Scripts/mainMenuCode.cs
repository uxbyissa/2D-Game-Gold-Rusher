using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainMenuCode : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void StartGame()
    {
        PlayClick();
        SceneManager.LoadScene("LevelSelect");
    }
    public void Credits()
    {
        PlayClick();
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        PlayClick();
        Application.Quit();
    }

    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
