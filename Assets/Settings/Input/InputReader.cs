using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    public event UnityAction<Vector2> PlayerOneMoveEvent = delegate { };
    public event UnityAction<Vector2> PlayerTwoMoveEvent = delegate { };
    public event UnityAction ShootEvent = delegate { };

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
        }
        _gameInput.Gameplay.Enable();
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
}
