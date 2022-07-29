using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothbrushInteraction : Interactable
{
    private AudioSource _audio;

    [SerializeField]
    private InputManager _input;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    //LateUpdate happens during Interaction
    private void LateUpdate()
    {
        if (!_audio.isPlaying)
            _input.EnableMovement();
    }

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        _audio.Play();
        _input.DisableMovement();
        gameObject.layer = 0;
    }
}
