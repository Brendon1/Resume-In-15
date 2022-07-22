using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    void Start()
    {
        //Setup title screen immediately
        titleName.text = "Resume In " + time;
    }

    #region Animation Functions
    public void StartTitleAnim()
    {
        _animation.Play(start, 0, 0.0f);
        StartCoroutine(DelayTime(2));
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

        EndTitleAnim();
    }
    #endregion
}
