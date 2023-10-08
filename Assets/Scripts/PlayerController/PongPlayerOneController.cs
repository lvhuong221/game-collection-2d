using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerOneController : PongPlayerMovement
{
    [SerializeField] private PongInputReader _inputReader;

    private void Start()
    {
        _inputReader.PlayerOneMoveEvent += InputReader_PlayerOneMoveEvent;   
    }

    private void InputReader_PlayerOneMoveEvent(Vector2 arg0)
    {
        UpdateMoveVector(arg0);
    }
}
