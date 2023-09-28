using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have bool arguement
/// </summary>
[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
public class BoolEventChannelSO : DescriptionBaseSO
{
    public event UnityAction<bool> OnEventRaised;

    public void Raise(bool value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        } else
        {
            Debug.LogWarning("A bool event was requested, but nobody picked it up. ", this);
        }
    }
}
