using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsInteractable : Interactable
{
    [SerializeField]
    private AudioSource _audio;

    /// <summary>
    /// Interact with pants
    /// </summary>
    protected override void Interact()
    {
        _audio.Play();
        gameObject.layer = 0;
        gameObject.transform.localPosition = new Vector3(300, 0, 0);
    }
}
