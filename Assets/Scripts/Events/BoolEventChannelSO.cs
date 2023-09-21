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
        OnEventRaised?.Invoke(value);
    }
}
