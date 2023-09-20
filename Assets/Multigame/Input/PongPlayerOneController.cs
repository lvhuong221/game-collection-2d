using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerOneController : MonoBehaviour
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

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDelta = speed * moveVector.y * Time.deltaTime;
        if (moveVector == Vector2.zero)
        {
            return;
        }
        if (transform.position.y + moveDelta >= topLimit)
        {
            transform.position = new Vector2(transform.position.x, topLimit);
            return;
        }
        if (transform.position.y + moveDelta <= bottomLimit)
        {
            transform.position = new Vector2(transform.position.x, bottomLimit);
            return;
        }
        transform.position = new Vector2(transform.position.x, transform.position.y + moveDelta);
    }

    private void UpdateMoveVector(Vector2 moveVector)
    {
        this.moveVector = moveVector;
    }
}
