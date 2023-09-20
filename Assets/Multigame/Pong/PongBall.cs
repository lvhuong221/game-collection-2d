using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private float speed = 10;

    private Vector2 nextRandomMovement;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
        indicator.SetActive(true);
        nextRandomMovement = new Vector2(Random.Range(-1f, 1f), Random.Range(-2f, .2f));
        transform.up = nextRandomMovement;
        Invoke("RandomeMovement", 1);
    }

    private void RandomeMovement()
    {
        indicator.SetActive(false);
        rgbd.AddForce(nextRandomMovement.normalized * speed, ForceMode2D.Impulse);
    }
}