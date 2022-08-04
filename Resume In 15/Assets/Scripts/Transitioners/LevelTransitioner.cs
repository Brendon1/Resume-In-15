using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    public Animator transition;
    private int levelToLoad;

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        transition.SetTrigger("FadeOut");
    }

    //IEnumerator LoadLevel(int levelIndex)
    //{
    //    transition.SetTrigger("FadeOut");

    //    yield return new WaitForSeconds(1);

    //    SceneManager.LoadScene(levelIndex);
    //}

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
