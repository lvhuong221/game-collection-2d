using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGameManager : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;

    private void Start()
    {
        _inputReader.EnableShootToMoveInput();
    }


}
