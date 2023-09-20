using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EventTrigger2D : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayerMask;
    [SerializeField] private ScriptableObjectArchitecture.GameEvent eventToTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((triggerLayerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            eventToTrigger.Raise();
        }
    }
}
