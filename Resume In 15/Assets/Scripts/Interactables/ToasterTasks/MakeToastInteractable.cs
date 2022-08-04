using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeToastInteractable : Interactable
{
    [SerializeField]
    private GameObject toast;

    [SerializeField]
    private InputManager _input;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        _audio.Play();
        gameObject.layer = 0;
        StartCoroutine(WaitForAudio());
    }

    IEnumerator WaitForAudio()
    {
        _input.DisableMovement();
        yield return new WaitForSeconds(_audio.clip.length - 2); //toast come out at "ding" sound :)))
        toast.SetActive(true);
        toast.layer = 6;
        _input.EnableMovement();
    }
}
