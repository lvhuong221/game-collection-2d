using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private TrailRenderer trail;
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

        nextRandomMovement = new Vector2(Random.Range(.2f, 1) * ((Random.value > 0.5) ? 1 : -1), Random.Range(-.2f, .2f));
        indicator.transform.up = nextRandomMovement;
        rgbd.velocity = Vector2.zero;
        DisableTrail();
        Invoke("RandomMovement", 1);
    }

    private void RandomMovement()
    {
        Debug.Log("Random movement" + nextRandomMovement.normalized);
        EnableTrail();
        indicator.SetActive(false);
        rgbd.velocity = nextRandomMovement.normalized * speed;
    }

    private void EnableTrail()
    {
        if(trail != null)
        {
            trail.enabled = true;
        }
    }

    private void DisableTrail()
    {
        if (trail != null)
        {
            trail.enabled = false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    PongPlayerMovement player = collision.gameObject.GetComponent<PongPlayerMovement>();
    //    if (player != null) 
    //    {
    //        Debug.Log("Add force: " + player.VelocityVector());
    //        rgbd.AddForce(player.VelocityVector());
    //    }
    //}
}
