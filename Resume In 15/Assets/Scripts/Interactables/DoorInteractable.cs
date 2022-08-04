using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractable : Interactable
{
    private AudioSource _audio;

    private Animator _animation;
    private readonly string openDoor = "OpenDoor";
    private readonly string closeDoor = "CloseDoor";
    private bool isOpen = false;
    
    void Awake()
    {
        _animation = GetComponentInParent<Animator>();
        if(!tag.Equals("ShowerDoor")) //it stays like this until I can get a good shower door sound
            _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        if (!isOpen)
        {
            prompt = "(Close Door)";
            isOpen = true;
            _animation.Play(openDoor);

            StartCoroutine(WaitForAnimation());
        }
        else
        {
            prompt = "(Open Door)";
            isOpen = false;
            _animation.Play(closeDoor);

            StartCoroutine(WaitForAnimation());
        }

        if (!tag.Equals("ShowerDoor"))
            _audio.Play();
    }

    #region Numerator
    /// <summary>
    /// This is here to ensure there are no graphical glitches when opening and closing door
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForAnimation()
    {
        gameObject.layer = 0;
        yield return new WaitForSeconds(2f);
        gameObject.layer = 6;
    }
    #endregion
}
