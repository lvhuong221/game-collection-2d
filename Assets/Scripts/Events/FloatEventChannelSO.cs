using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannelSO : DescriptionBaseSO
{
    public UnityAction<float> OnFloatRequested;
    public void Raise(float value)
    {
        if (OnFloatRequested != null)
        {
            OnFloatRequested.Invoke(value);
        } else
        {
            Debug.LogWarning("A float event was requested, but nobody picked it up");
        }
    }
}
