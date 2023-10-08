using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GoalController : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO playerReachGoalEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ShootToMovePlayerController>() != null)
        {
            playerReachGoalEvent.Raise();
        }
    }
}
