using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ShootToMoveInputReader", menuName = "Input/Input Reader/Shoot to move")]
public class ShootToMoveInputReader : ScriptableObject, GameInput.IShootToMoveActions
{
    //public event UnityAction<Vector2> mousePositionEvent;
    public event UnityAction shootEvent;

    public Vector2 MousePosition {  get; private set; }
    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.ShootToMove.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        DisableAllInput();
    }
    public void DisableAllInput()
    {
        _gameInput.ShootToMove.Disable();
    }

    public void EnableShootToMoveInput()
    {
        _gameInput.ShootToMove.Enable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            shootEvent.Invoke();
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
        if (context.phase == InputActionPhase.Performed)
        {
        }
    }
}
