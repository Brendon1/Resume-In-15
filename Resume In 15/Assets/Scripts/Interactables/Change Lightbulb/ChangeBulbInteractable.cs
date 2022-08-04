using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeBulbInteractable : Interactable
{
    /// <summary>
    /// Private fields related to task. (This is getting kinda spaghetti-ish whooops...)
    /// </summary>
    private AudioSource _audio;
    private bool taskDone;
    private Coroutine interval;
    private bool canFlicker;

    [SerializeField]
    private TextMeshProUGUI Task2; //This is to save the "Change Lightbulb" Task in memory

    [SerializeField]
    private InputManager _input;

    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private GameObject currentBulb;
    [SerializeField]
    private GameObject replacementBulb;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        taskDone = false;
        canFlicker = true;
    }

    void Update()
    {
        if (canFlicker)
            interval = StartCoroutine(FlickerInterval());

        //This is here to ensure that the order events remains: "Unscrew Lightbulb --> Grab Lightbulb --> Screw In Lightbulb"
        if (!replacementBulb.activeSelf && prompt.Equals("(Screw In Lightbulb)"))
        {
            gameObject.layer = 6;
            task = Task2;
        }

        //This is here to ensure and task is no longer interactable since the replacement bulb will always be inactive now
        if(taskDone)
            gameObject.layer = 0;
    }

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        // ---------------- These conditionals control the state of current interaction ----------------------- \\
        if(prompt.Equals("(Unscrew Lightbulb)"))
        {
            _audio.Play();
            StartCoroutine(WaitForAudio(false)); //start the audio, pause player, disable the pointlight and bulb, end flickering

            gameObject.layer = 0;
            prompt = "(Screw In Lightbulb)"; //change prompt for second interaction
        }
        if (prompt.Equals("(Screw In Lightbulb)") && gameObject.layer == 6)
        {
            _audio.Play();
            StartCoroutine(WaitForAudio(true)); //start the audio, pause player, enable pointlight and bulb

            taskDone = true;
        }
    }

    #region Numerators
    /// <summary>
    /// This gives off the "flicker" effect during update
    /// </summary>
    /// <returns></returns>
    IEnumerator FlickerInterval()
    {
        pointLight.enabled = true;
        yield return new WaitForSeconds(1f);
        pointLight.enabled = false;
    }

    IEnumerator WaitForAudio(bool flag)
    {
        _input.DisableMovement();
        yield return new WaitForSeconds(_audio.clip.length);
        _input.EnableMovement();
        pointLight.enabled = flag;
        currentBulb.SetActive(flag);
        canFlicker = false;
    }
    #endregion
}
