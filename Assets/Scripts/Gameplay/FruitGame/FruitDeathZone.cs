using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDeathZone : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO inDangerEvent;
    [SerializeField] private VoidEventChannelSO gameOverEvent;

    [SerializeField] private float gracePeriod = 3.0f;

    int totalInZone = 0;
    float elapsed = 0;

    private bool inDanger;
    bool InDanger {
        get { 
            return inDanger;
        }
        set
        {
            if(inDanger != value)
            {
                inDangerEvent.Raise(value);
            }
            InDanger = value;
        }
    }

    private void Update()
    {
        if(totalInZone > 0)
        {
            if(InDanger == false)
            {
                InDanger = true;
            }
            elapsed += Time.deltaTime;
            if(elapsed > gracePeriod)
            {
                gameOverEvent.Raise();
            }
        } else
        {
            if(InDanger == true)
            {
                InDanger = false;
            }

            elapsed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<FruitController>() != null)
        {
            totalInZone++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FruitController>() != null)
        {
            totalInZone--;
        }
    }
}
