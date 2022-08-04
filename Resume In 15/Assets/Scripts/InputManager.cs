using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnGroundActions onGroundActions;

    private PlayerMovement movement;
    private PlayerCamera _camera;
    private PlayerUI _ui;

    [SerializeField]
    private LevelTransitioner transitioner;

    //Control player state (movement and camera)
    private bool canMove;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onGroundActions = playerInput.OnGround;
        movement = GetComponent<PlayerMovement>();
        _camera = GetComponent<PlayerCamera>();
        _ui = GetComponent<PlayerUI>();

        _camera.CursorLockState(true);
        EnableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            movement.Movement(onGroundActions.Movement.ReadValue<Vector2>());

        _ui.AllFilesObtained();

        //Do this to ensure that the player finishes all tasks and then transition to next level
        if (_ui.FinishedLastTask())
        {
            transitioner.FadeToNextLevel();
            _camera.CursorLockState(false);
            DisableMovement();
        }
    }

    private void LateUpdate()
    {
        _camera.CameraLook(onGroundActions.CameraLook.ReadValue<Vector2>());
    }

    #region Enable or Disable GroundActions
    private void OnEnable()
    {
        onGroundActions.Enable();
    }
    
    private void OnDisable()
    {
        onGroundActions.Disable();
    }

    public bool EnableMovement()
    {
        canMove = true;
        return canMove;
    }
    
    public bool DisableMovement()
    {
        canMove = false;
        return canMove;
    }
    #endregion
}
