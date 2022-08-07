using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Player Input (Input System)
    private PlayerInput playerInput;
    public PlayerInput.OnGroundActions onGroundActions;
    public PlayerInput.MenuActions menuActions;

    //Scripts attached to player
    private PlayerMovement movement;
    private PlayerCamera _camera;
    private PlayerUI _ui;
    private PauseMenu pauseMenu;

    [Header("Canvas Overlay Fields")] //These are here for level transitioning and pause menu functionality
    [SerializeField] private LevelTransitioner transitioner;
    [SerializeField] private GameObject TasklistObject;
    [SerializeField] private GameObject UIObject;

    //Control player state (movement and camera)
    private bool canMove;

    // Start is called before the first frame update
    void Awake()
    {
        //Input System
        playerInput = new PlayerInput();
        onGroundActions = playerInput.OnGround;
        menuActions = playerInput.Menu;

        //Code Components attached to Player
        movement = GetComponent<PlayerMovement>();
        _camera = GetComponent<PlayerCamera>();
        _ui = GetComponent<PlayerUI>();
        pauseMenu = GetComponent<PauseMenu>();

        //Extra Modes to ensure gameplay starts correctly
        _camera.CursorLockState(true);
        EnableMovement();
    }

    /// <summary>
    /// Manages Player Movement and Menu Actions
    /// </summary>
    void Update()
    {
        //Check if player pressed pause
        if(menuActions.Pause.IsPressed() && !pauseMenu.IsPaused())
        {
            pauseMenu.Pause();
            PlayGame(false);
        }

        //Check if player is paused, otherwise play game
        if(!pauseMenu.IsPaused())
        {
            //Resume
            PlayGame(true);

            //Check Movement perframe, but only if the player can move
            if (canMove)
                movement.Movement(onGroundActions.Movement.ReadValue<Vector2>());

            //This ensures files are collected and updated to "inventory"
            _ui.AllFilesObtained();

            //Do this to ensure that the player finishes all tasks and then transition to next level
            if (_ui.FinishedLastTask())
            {
                transitioner.FadeToNextLevel();
                _camera.CursorLockState(false);
                DisableMovement();
            }
        }
    }

    /// <summary>
    /// Manages Camera Control
    /// </summary>
    private void LateUpdate()
    {
        //Check Camera Control perframe after movement frames have been registered (Avoids camera stuttering
        if(!pauseMenu.IsPaused())
            _camera.CameraLook(onGroundActions.CameraLook.ReadValue<Vector2>());
    }

    #region Enable or Disable GroundActions
    private void OnEnable()
    {
        onGroundActions.Enable();
        menuActions.Enable();
    }
    
    private void OnDisable()
    {
        onGroundActions.Disable();
        menuActions.Disable();
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

    #region Helper Methods
    private void PlayGame(bool flag)
    {
        TasklistObject.SetActive(flag);
        UIObject.SetActive(flag);
        _camera.CursorLockState(flag);
    }
    #endregion
}
