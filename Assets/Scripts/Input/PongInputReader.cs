using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PongInputReader", menuName = "Input/Input Reader/Pong")]
public class PongInputReader : ScriptableObject, GameInput.IPongGameplayActions
{
    public event UnityAction<Vector2> PlayerOneMoveEvent = delegate { };
    public event UnityAction<Vector2> PlayerTwoMoveEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction ShootEvent = delegate { };

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.PongGameplay.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnPlayer1Move(InputAction.CallbackContext context)
    {
        PlayerOneMoveEvent.Invoke(context.ReadValue<Vector2>());

    }

    public void OnPlayer2Move(InputAction.CallbackContext context)
    {
        PlayerTwoMoveEvent.Invoke(context.ReadValue<Vector2>());

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent.Invoke();
        }
    }

    public void DisableAllInput()
    {
        _gameInput.Disable();
    }

    public void EnableGameplayInput()
    {
        _gameInput.Enable();
    }

}
