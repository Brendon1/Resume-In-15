using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class TitleScreenTransitioner : MonoBehaviour
{
    //Corresponding animation controllers
    [Header("Animation Controllers")]
    public Animator _animation;
    public LevelTransitioner transitioner;

    //Animation nametags
    private readonly string start = "TitleScreen_Start";
    private readonly string end = "TitleScreen_End";

    //TitleScreen Labels
    [Header("Titlescreen Labels")]
    [SerializeField] private TextMeshProUGUI titleName;
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    private float time = 15;

    //Ad to show from list
    [Header("AD to Show Parameters")]
    [SerializeField] private GameObject randomAd;
    [SerializeField] private TextMeshProUGUI countdownTimer;
    private Vector2 adSize = new Vector2(1920, 1080);
    private List<Object> optionalList;
    //private List<Object> adList;

    void Start()
    {
        //Ensure mouse cursor is there at all times
        Cursor.lockState = CursorLockMode.None;

        //Setup title screen immediately
        titleName.text = "Resume In " + time;

        // Read in all ad videos
        optionalList = new List<Object>(Resources.LoadAll("OptionalVideos"));
        //adList = new List<Object>(Resources.LoadAll("AdVideos"));

        //Screen.SetResolution(1920, 1080, true);
    }

    #region Animation Functions
    public void StartTitleAnim()
    {
        //Start Animation First and Disable All Buttons
        _animation.Play(start, 0, 0.0f);
        startButton.enabled = false;
        quitButton.enabled = false;

        //Then Start Ad
        StartCoroutine(DelayForAdPopUp(1));

        //Start Title Screen Countdown
        StartCoroutine(Resume_In_15_Timer());
    }

    private void EndTitleAnim()
    {
        _animation.Play(end, 0, 0.0f);

        //Transition During Ending Animation
        transitioner.FadeToNextLevel();
    }
    #endregion

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
            titleName.text = "Resume In " + time;

            yield return new WaitForSeconds(1f);

            time--;
        }

        time = 0;
        EndTitleAnim();
    }
    #endregion

    #region Spawn Ads
    public void ShowRandomAd(GameObject ad)
    {
        if (!ad.activeSelf && ad != null)
        {
            //Disable Buttons to prevent player from progression during ad
            DisableButtons();

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

    #region Enable/Disable Buttons
    public void EnableButtons()
    {
        if(countdownTimer.text.Equals("X"))
        {
            startButton.enabled = true;
            quitButton.enabled = true;
        }
    }
    
    public void DisableButtons()
    {
        startButton.enabled = false;
        quitButton.enabled = false;
    }
    #endregion
}
