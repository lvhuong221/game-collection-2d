using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerOneController : PongPlayerMovement
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float speed = 5;

    private Vector2 moveVector;
    private float topLimit = 2.9f, bottomLimit = -2.9f;
    private float moveDelta = 0;

    private void Start()
    {
        _inputReader.PlayerOneMoveEvent += InputReader_PlayerOneMoveEvent;   
    }

    private void InputReader_PlayerOneMoveEvent(Vector2 arg0)
    {
        UpdateMoveVector(arg0);
    }
}
