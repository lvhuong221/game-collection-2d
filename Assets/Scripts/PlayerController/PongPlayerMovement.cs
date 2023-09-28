using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float speed = 5;

    private Vector2 moveVector;
    private Vector2 lastFramePosition;
    private float topLimit = 4f, bottomLimit = -4f;
    private float moveDelta = 0;


    // Start is called before the first frame update
    void Start()
    {
        lastFramePosition = transform.position;
    }

    private void Update()
    {
        BeforeMove();
        Move();
        AfterMove();
    }

    protected virtual void BeforeMove(){ }

    protected virtual void AfterMove() 
    {
    }

    private void Move()
    {
        lastFramePosition = transform.position;

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

    public Vector2 VelocityVector()
    {
        return (Vector2)transform.position - lastFramePosition;
    }
}
