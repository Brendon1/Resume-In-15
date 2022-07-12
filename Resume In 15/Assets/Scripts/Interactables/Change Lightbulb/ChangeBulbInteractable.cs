using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBulbInteractable : Interactable
{
    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private GameObject bulb;

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        pointLight.enabled = true;
        bulb.SetActive(true);
        gameObject.layer = 0;
    }
}
