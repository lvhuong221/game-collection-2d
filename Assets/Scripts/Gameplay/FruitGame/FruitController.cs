using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FruitController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FruitSO thisFruitSO;

    public FruitSO FruitSO { get => thisFruitSO; }
    public UnityAction<FruitController, FruitController> combineRequest;

    public bool IsCombined {  get; private set; }

    private void Awake()
    {
        IsCombined = false;
    }

    public void Hold()
    {
        rb.isKinematic = true;
    }

    public void Release()
    {
        rb.isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FruitController fruitToCombine = collision.gameObject.GetComponent<FruitController>();
        if( fruitToCombine != null && fruitToCombine.IsCombined == false && fruitToCombine.FruitSO == FruitSO)
        {
            IsCombined = true;
            //Debug.Log("Request combine " + fruitToCombine + " and " + this);
            combineRequest?.Invoke(fruitToCombine, this);
        }
    }
}
