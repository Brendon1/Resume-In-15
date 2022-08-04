using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabToast : Interactable
{
    [SerializeField]
    private GameObject toaster;

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        toaster.layer = 6;
        Destroy(gameObject);
    }
}
