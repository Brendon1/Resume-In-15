using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFile : Interactable
{

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        Destroy(gameObject);
    }
}
