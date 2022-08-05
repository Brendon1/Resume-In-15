using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private AudioSource pauseSound;
    private bool isPaused = false;

    void Awake()
    {
        pauseMenu.SetActive(false); //Ensure pause menu is never shown when gameplay starts
        pauseSound = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Resume Game
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        //Time.timeScale = 1f; //unfreeze time
        //AudioListener.pause = false; //unfreeze sounds

        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Go back to main menu
    /// </summary>
    public void MainMenu()
    {
        isPaused = false;
        //Time.timeScale = 1f; //unfreeze time
        //AudioListener.pause = false; //unfreeze sounds

        SceneManager.LoadScene(0); //goes to titlescreen
    }

    /// <summary>
    /// Pause Game
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        pauseSound.Play(); //play funneee sound heehee hoho haha giggity gafaw!
        //Time.timeScale = 0f; //freeze time
        //AudioListener.pause = true; //freeze sounds

        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Returns current state of pause machine
    /// </summary>
    /// <returns></returns>
    public bool IsPaused()
    {
        return isPaused;
    }
}
