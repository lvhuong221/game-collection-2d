using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for scene-loading evnets.
/// Take a GameSceneSO of the mini game or menu that need to be loaded, and a bool to specifu if a loading screen needs to display
/// </summary>
[CreateAssetMenu(menuName = "Events/Int event Channel")]
public class IntEventChannelSO : DescriptionBaseSO
{
    public UnityAction<int> OnEventRaised;

    public void Raise(int value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        } else
        {
            Debug.LogWarning("A Scene loading was requested, but nobody picked it up. " +
                "Check why there is no SceneLoader already present, " +
                "and make sure it's listening on this Load Event channel.");
        }
    }
}
