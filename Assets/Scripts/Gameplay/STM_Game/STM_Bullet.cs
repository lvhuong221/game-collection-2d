using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class STM_Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
    }

    public void SetDirection(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<IDestructable>() != null)
        {
            collision.gameObject.GetComponent<IDestructable>().Destroy();
        }
        gameObject.SetActive(false);
    }
}
