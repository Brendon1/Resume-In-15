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

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onGroundActions = playerInput.OnGround;
        movement = GetComponent<PlayerMovement>();
        _camera = GetComponent<PlayerCamera>();
        _ui = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.Movement(onGroundActions.Movement.ReadValue<Vector2>());

        //Do this to ensure that the player finishes all tasks and then transition to next level
        if(_ui.allTasksComplete())
        {
            transitioner.FadeToNextLevel();
        }
    }

    private void LateUpdate()
    {
        _camera.CameraLook(onGroundActions.CameraLook.ReadValue<Vector2>());
    }

    #region Enable or Disable Movement
    private void OnEnable()
    {
        onGroundActions.Enable();
    }
    
    private void OnDisable()
    {
        onGroundActions.Disable();
    }
    #endregion
}
