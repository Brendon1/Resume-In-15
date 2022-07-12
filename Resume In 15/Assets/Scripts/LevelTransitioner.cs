using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    //private bool SceneExists(string name)
    //{
    //    List<string> scenesInBuild = new List<string>();
    //    for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
    //    {
    //        string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
    //        int lastSlash = scenePath.LastIndexOf("/");
    //        scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
    //    }
    //    return scenesInBuild.Any(t => t == name);
    //}

    public void QuitApplication()
    {
        Application.Quit();
    }
}
