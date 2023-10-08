using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMousePosition : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;

    Vector3 _mousePosition;

    private void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1f));
        transform.up = new Vector3(_mousePosition.x, _mousePosition.y, 0) - transform.position;
    }


}
