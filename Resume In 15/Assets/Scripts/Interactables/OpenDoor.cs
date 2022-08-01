using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{

    private AudioSource _audio;

    private Animator _animation;
    private readonly string openDoor = "OpenDoor";
    
    void Awake()
    {
        _animation = GetComponentInParent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        _animation.Play(openDoor);
        _audio.Play();
        gameObject.layer = 0;
    }
}
