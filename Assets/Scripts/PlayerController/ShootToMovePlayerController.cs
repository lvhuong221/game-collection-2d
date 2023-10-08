using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToMovePlayerController : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;
    [SerializeField] Rigidbody2D _body2D;
    [SerializeField] GameObjectVariable playerObject; 

    [SerializeField, Range(1, 1000)] float jumpForce = 100;

    private void Start()
    {
        playerObject.Value = gameObject;
    }

    private void OnEnable()
    {
        _inputReader.shootEvent += OnShootEvent;
    }

    private void OnDisable()
    {
        _inputReader.shootEvent -= OnShootEvent;
    }

    private void Update()
    {

    }

    private void OnShootEvent()
    {
        Vector2 shootDirection = -(Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1f)) - transform.position);
        //Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1f)), transform.position, Color.black, 20);

        _body2D.AddForce(shootDirection.normalized * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<GoalController>() != null)
        {
            Debug.Log("Player hit goal");
            _body2D.isKinematic = true;
            _body2D.velocity = Vector2.zero;
        }
    }
}
