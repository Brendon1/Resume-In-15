using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float gravity = -9.8f;
    public float speed = 10f;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void Movement(Vector2 input)
    {
        //Handle Movement for player
        Vector3 direction = Vector3.zero;
        direction.x = input.x;
        direction.z = input.y;
        controller.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);

        #region Retrieve Movement
        if(direction.x != 0 || direction.z != 0 && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        #endregion

        //Ensure player isn't contnously going down even when grounded
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -5f;
        }

        //Handle gravity of player
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
