using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerInteractable : Interactable
{
    [SerializeField]
    private AudioSource _audio;

    [SerializeField]
    private InputManager _input;

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        _audio.Play();
        StartCoroutine(WaitForAudio());
        gameObject.layer = 0;
    }

    IEnumerator WaitForAudio()
    {
        _input.DisableMovement();
        yield return new WaitForSeconds(_audio.clip.length);
        _input.EnableMovement();
    }
}
