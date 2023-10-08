using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DuckHuntUIManager : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;

    private void Start()
    {
        if(eventSystem != null && eventSystem != EventSystem.current) 
        {
            Destroy(eventSystem.gameObject);
        }
    }
}
