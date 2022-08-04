using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothbrushInteraction : Interactable
{
    private AudioSource _audio;

    [SerializeField]
    private InputManager _input;

    private Vector3 currentPos;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        currentPos = transform.position;
    }

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        _audio.Play();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 20, gameObject.transform.position.y, gameObject.transform.position.z); //hide the toothbrush

        StartCoroutine(WaitForAudio());
        gameObject.layer = 0;
    }

    IEnumerator WaitForAudio()
    {
        _input.DisableMovement();
        yield return new WaitForSeconds(_audio.clip.length);
        gameObject.transform.position = currentPos; //show it again
        _input.EnableMovement();
    }
}
