using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class TitleScreenTransitioner : MonoBehaviour
{
    //Corresponding animation controllers
    public Animator _animation;
    public LevelTransitioner transitioner;

    //Animation nametags
    private readonly string start = "TitleScreen_Start";
    private readonly string end = "TitleScreen_End";

    //Title "Resume In 15" setters for future countdown
    [SerializeField] private TextMeshProUGUI titleName;
    private float time = 15;

    [SerializeField] private GameObject randomAd;
    private Vector2 adSize = new Vector2(1920, 1080);
    private List<Object> optionalList;
    //private List<Object> adList;

    void Start()
    {
        //Setup title screen immediately
        titleName.text = "Resume In " + time;

        // Read in all ad videos
        optionalList = new List<Object>(Resources.LoadAll("OptionalVideos"));
        //adList = new List<Object>(Resources.LoadAll("AdVideos"));
    }

    #region Animation Functions
    public void StartTitleAnim()
    {
        _animation.Play(start, 0, 0.0f);
        StartCoroutine(DelayTime(2));
        ShowRandomAd(randomAd);
        StartCoroutine(Resume_In_15_Timer());
    }

    private void EndTitleAnim()
    {
        _animation.Play(end, 0, 0.0f);
        transitioner.FadeToNextLevel();
    }
    #endregion

    #region Numerators
    IEnumerator DelayTime(int timer)
    {
        yield return new WaitForSeconds(timer);
    }

    IEnumerator Resume_In_15_Timer()
    {
        while (time > 0)
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
        // Create new render texture
        //var rendTexture = new RenderTexture(Screen.width, Screen.height, 24);
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

    //public void ShowRandomSmallAd()
    //{
    //    // Create new render texture
    //    //var rendTexture = new RenderTexture(Screen.width, Screen.height, 24);
    //    var rendTexture = new RenderTexture((int)adSize.x, (int)adSize.y, 24);

    //    // Add texture to ad obj raw image
    //    randomSmallAd.GetComponent<RawImage>().texture = rendTexture;

    //    // Add texture to ad obj video player
    //    randomSmallAd.GetComponent<VideoPlayer>().targetTexture = rendTexture;

    //    // Select random video from list
    //    int index = Random.Range(0, adList.Count);

    //    // add video clip to video player
    //    randomSmallAd.GetComponent<VideoPlayer>().clip = (VideoClip)adList[index];

    //    randomSmallAd.SetActive(true);
    //}
    #endregion
}