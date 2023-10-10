using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruit : MonoBehaviour
{
    [SerializeField] ShootToMoveInputReader _inputReader;
    [SerializeField, Range(1, 20)] private float _spawnRange = 1;

    [SerializeField] GameObject tempPrefab;
    [SerializeField] FruitCollectionSO fruitCollection;
    [SerializeField] IntEventChannelSO gainScoreEvent;

    FruitController currentFruit;
    FruitController nextFruit;

    private float leftClamp, rightClamp;
    private bool _canSpawn = true;

    private Dictionary<FruitSO, List<FruitController>> fruitPool;

    private void Awake()
    {
        leftClamp = transform.position.x - _spawnRange;
        rightClamp = transform.position.x + _spawnRange;
        SpawnRandomeFruitAndHold();
    }

    private void OnEnable()
    {
        _inputReader.mousePositionEvent += OnMouseMove;
        _inputReader.shootEvent += OnDropFruitEvent;
    }


    private void OnDisable()
    {
        _inputReader.mousePositionEvent -= OnMouseMove;
        _inputReader.shootEvent -= OnDropFruitEvent;
    }

    private void Update()
    {
        OnMouseMove(_inputReader.MousePosition);
    }

    private void OnDropFruitEvent()
    {
        if (_canSpawn == false)
            return;

        _canSpawn = false;
        currentFruit.Release();
        currentFruit = null;
        Invoke("SpawnRandomeFruitAndHold", 1);
    }

    private void SpawnRandomeFruitAndHold()
    {
        _canSpawn = true;
        FruitSO fruitToSpawn = fruitCollection.GetRandomFruitToSpawn();

        currentFruit = Instantiate(fruitToSpawn.prefab).GetComponent<FruitController>();
        currentFruit.Hold();
        currentFruit.combineRequest += CombineFruit;
    }

    private void CombineFruit(FruitController fruit1, FruitController fruit2)
    {
        FruitSO combineResult = fruit1.FruitSO.combineResult;

        // put new object at the middle of the 2
        GameObject newFruitObject = Instantiate(combineResult.prefab, 
            new Vector3((fruit1.transform.position.x + fruit2.transform.position.x)/2,
                (fruit1.transform.position.y + fruit2.transform.position.y) / 2,
                0), Quaternion.identity);
        FruitController newFruit = newFruitObject.GetComponent<FruitController>();
        newFruit.combineRequest += CombineFruit;

        //Debug.Log("handle combine " + fruit1 + " and " + fruit1 + " into " + newFruit);
        gainScoreEvent.Raise(combineResult.score);

        fruit1.combineRequest -= CombineFruit;
        fruit2.combineRequest -= CombineFruit;
        Destroy(fruit1.gameObject);
        Destroy(fruit2.gameObject);
    }

    private void OnMouseMove(Vector2 value)
    {
        if (currentFruit == null)
            return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1f));
        currentFruit.transform.position = new Vector3(Mathf.Clamp(mousePos.x, leftClamp, rightClamp), transform.position.y);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position - Vector3.right * _spawnRange, transform.position + Vector3.right * _spawnRange);
    }
}
