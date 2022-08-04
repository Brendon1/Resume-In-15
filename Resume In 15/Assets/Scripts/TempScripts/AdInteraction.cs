using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdInteraction : Interactable
{
    //private string prompt = "(Close Ad)";
    private void Start() {
        prompt = "(Close Ad)";
    }
    protected override void Interact()
    {
        Destroy(transform.parent.gameObject);
    }
}
