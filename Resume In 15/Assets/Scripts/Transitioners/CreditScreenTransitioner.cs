using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreditScreenTransitioner : MonoBehaviour
{

    //Corresponding animation controllers
    [Header("Animation Controllers")]
    public Animator _animation;
    public LevelTransitioner transitioner;

    //Animation nametags
    private readonly string start = "CreditsStart";
    private readonly string end = "CreditsEnd";

    //CreditScreen Labels
    [Header("CreditLabels Labels")]
    [SerializeField] private TextMeshProUGUI titleName;
    [SerializeField] private Button titleButton; //for when the text switches to "skip"
    private float time = 15;

    // Start is called before the first frame update
    void Start()
    {
        //Setup title screen immediately
        titleName.text = "Resume In " + time;

        //Start the Credits Roll after 3 seconds
        Invoke("StartCreditsAnim", 3);
    }

    #region Animation Functions
    public void StartCreditsAnim()
    {
        //Start Animation First
        _animation.Play(start, 0, 0.0f);

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
    IEnumerator Delay(int timer)
    {
        yield return new WaitForSeconds(timer);
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
        titleName.text = "Skip  >>";
        titleButton.enabled = true;
    }
    #endregion

    #region Skip Credits

    #endregion
}
