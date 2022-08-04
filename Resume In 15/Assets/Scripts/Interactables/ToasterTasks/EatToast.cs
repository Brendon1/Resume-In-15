using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatToast : Interactable
{

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        gameObject.SetActive(false);
        gameObject.layer = 0;
    }
}
