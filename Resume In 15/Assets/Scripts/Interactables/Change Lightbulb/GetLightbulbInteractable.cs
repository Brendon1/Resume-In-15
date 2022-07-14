using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLightbulbInteractable : Interactable
{
    [SerializeField]
    private GameObject bulbContainer;

    /// <summary>
    /// Use this function to make any type of interaction
    /// </summary>
    protected override void Interact()
    {
        bulbContainer.layer = 6;
        Destroy(gameObject);
    }
}
