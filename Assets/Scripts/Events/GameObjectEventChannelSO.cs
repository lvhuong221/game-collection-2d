using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have gameObject as arguement
/// </summary>
[CreateAssetMenu(menuName = "Events/GameObject Event Channel")]
public class GameObjectEventChannelSO : DescriptionBaseSO
{
    public event UnityAction<GameObject> OnEventRaised;

    public void Raise(GameObject value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        } else
        {
            Debug.LogWarning("A gameObject event was requested, but nobody picked it up. ", this);
        }
    }
}
