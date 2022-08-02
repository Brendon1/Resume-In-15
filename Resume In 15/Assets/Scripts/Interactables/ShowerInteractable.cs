using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerInteractable : Interactable
{
    [SerializeField]
    private AudioSource _audio;

    [SerializeField]
    private InputManager _input;

    [Header("Effects")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject steam;

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        _audio.Play();
        StartEffects(true);
        StartCoroutine(WaitForAudio());
        gameObject.layer = 0;
    }

    IEnumerator WaitForAudio()
    {
        _input.DisableMovement();
        yield return new WaitForSeconds(_audio.clip.length);
        StartEffects(false);
        _input.EnableMovement();
    }

    #region Effects Trigger
    private void StartEffects(bool flag)
    {
        water.SetActive(flag);
        steam.SetActive(flag);
    }
    #endregion
}
