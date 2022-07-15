using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float distance = 5f; //distance between player and interactable

    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<PlayerCamera>()._camera; //grab current camera
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ensure it stays empty when not colliding
        playerUI.UpdateTextAndCrosshair(string.Empty);

        //Raycast
        CreateRaycast(distance);

        //Interaction
        //PlayerInteract(hit);
    }

    private void CreateRaycast(float length)
    {
        //Center cast in camera
        RaycastHit hit;
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out hit, length, mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                //Give interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //Update the text on screen
                playerUI.UpdateTextAndCrosshair(hit.collider.GetComponent<Interactable>().prompt);

                //Give the object an outline shader on hover
                //GetComponent<Renderer>().material.SetFloat("_Outline", 0.2f);

                //Check for interact press
                if (inputManager.onGroundActions.Interact.triggered)
                {
                    interactable.BaseInteract(); //this will run the "Interact" function in the overrwritten interactable object
                    interactable.BaseCompleteTask(); //Complete the task associated with interactable
                }
            }
        }

        //return hit;
    }

    //private void PlayerInteract(RaycastHit hit)
    //{
    //    if (hit.collider.GetComponent<Interactable>() != null)
    //    {
    //        Debug.Log(hit.collider.GetComponent<Interactable>().prompt);
    //    }
    //}
}
