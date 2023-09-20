using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerTwoController : PongPlayerMovement
{
    [SerializeField] private InputReader _inputReader;


    private void Start()
    {
        _inputReader.PlayerTwoMoveEvent += InputReader_PlayerTwoMoveEvent;
    }

    private void InputReader_PlayerTwoMoveEvent(Vector2 arg0)
    {
        UpdateMoveVector(arg0);
    }

}
