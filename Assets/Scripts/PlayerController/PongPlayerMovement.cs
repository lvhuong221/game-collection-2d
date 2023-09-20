using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float speed = 5;

    private Vector2 moveVector;
    private float topLimit = 2.9f, bottomLimit = -2.9f;
    private float moveDelta = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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

    protected void UpdateMoveVector(Vector2 moveVector)
    {
        this.moveVector = moveVector;
    }
}
