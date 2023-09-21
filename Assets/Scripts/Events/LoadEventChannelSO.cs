using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for scene-loading evnets.
/// Take a GameSceneSO of the mini game or menu that need to be loaded, and a bool to specifu if a loading screen needs to display
/// </summary>
[CreateAssetMenu(menuName = "Events/Load Event Channel")]
public class LoadEventChannelSO : DescriptionBaseSO
{
    public UnityAction<GameSceneSO, bool, bool> OnLoadingRequested;

    public void Raise(GameSceneSO sceneToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (OnLoadingRequested != null)
        {
            OnLoadingRequested.Invoke(sceneToLoad, showLoadingScreen, fadeScreen);
        } else
        {
            Debug.LogWarning("A Scene loading was requested, but nobody picked it up. " +
                "Check why there is no SceneLoader already present, " +
                "and make sure it's listening on this Load Event channel.");
        }
    }
}
