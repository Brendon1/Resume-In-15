using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseAnimation : MonoBehaviour
{
    private Animator animator;

    private readonly string isResumed = "IsResumed"; //Controls the state of animation
    private float time = 15;

    [Header("PauseMenu")]
    [SerializeField] private PauseMenu pauseMenu;

    //PauseScreen Labels
    [Header("Pausescreen Labels")]
    [SerializeField] private TextMeshProUGUI resumeButton;

    //Ad to show from list
    [Header("AD to Show Parameters")]
    [SerializeField] private GameObject randomAd;
    private Vector2 adSize = new Vector2(1920, 1080);
    private List<Object> optionalList;

    void OnEnable()
    {
        animator = GetComponent<Animator>();

        //Reset Changed values back to idle state values
        resumeButton.text = "Resume";
        time = 15;
        randomAd.SetActive(false);

        // Read in all ad videos
        optionalList = new List<Object>(Resources.LoadAll("OptionalVideos"));
    }

    public void StartPauseScreenResumeAnim()
    {
        //Play Animation
        animator.SetBool(isResumed, true);

        //Then Start Ad
        StartCoroutine(DelayForAdPopUp(1));

        //Start Title Screen Countdown
        StartCoroutine(Resume_In_15_Timer());
    }

    #region Numerators
    IEnumerator DelayForAdPopUp(int timer)
    {
        yield return new WaitForSeconds(timer);
        ShowRandomAd(randomAd);
    }

    IEnumerator Resume_In_15_Timer()
    {
        while (time >= 0)
        {
            resumeButton.text = "Resume In " + time;

            yield return new WaitForSeconds(1f);

            time--;
        }

        time = 0;
        animator.SetBool(isResumed, false);
        #region Bug Fix (Don't touch)
        yield return new WaitForSeconds(.0001f); //this is here to ensure it goes back to the idle state (I don't know why it doesn't just immediately update or go back to idle without this)
        #endregion
        pauseMenu.Resume();
    }
    #endregion

    #region Spawn Ads
    public void ShowRandomAd(GameObject ad)
    {
        if (!ad.activeSelf && ad != null)
        {
            // Create new render texture
            var rendTexture = new RenderTexture((int)adSize.x, (int)adSize.y, 24);

            // Add texture to ad obj raw image
            ad.GetComponent<RawImage>().texture = rendTexture;

            // Add texture to ad obj video player
            ad.GetComponent<VideoPlayer>().targetTexture = rendTexture;

            // Select random video from list
            int index = Random.Range(0, optionalList.Count);

            // add video clip to video player
            ad.GetComponent<VideoPlayer>().clip = (VideoClip)optionalList[index];

            ad.SetActive(true);
        }
    }
    #endregion
}
