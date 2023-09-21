using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguement
/// </summary>
[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : DescriptionBaseSO
{
    public event UnityAction OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
}
