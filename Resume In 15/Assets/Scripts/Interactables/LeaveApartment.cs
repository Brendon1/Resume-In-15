using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveApartment : Interactable
{
    [SerializeField] private PlayerUI ui;

    void Update()
    {
        if (ui.AllPreviousTasksComplete())
            gameObject.layer = 6;
    }

    protected override void Interact()
    {
        gameObject.layer = 0;
    }
}
