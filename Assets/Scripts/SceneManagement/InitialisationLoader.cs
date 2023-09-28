using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>
public class InitialisationLoader : MonoBehaviour
{
    [SerializeField] private GameSceneSO _managersScene = default;
    [SerializeField] private GameSceneSO _menuToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private AssetReference _menuLoadChannel = default;

    private void Start()
    {
        _managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
    }

    private void LoadEventChannel(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        _menuLoadChannel.LoadAssetAsync<LoadSceneEventChannelSO>().Completed += InitialisationLoader_Completed;
    }

    private void InitialisationLoader_Completed(AsyncOperationHandle<LoadSceneEventChannelSO> handle)
    {
        handle.Result.Raise(_menuToLoad, true);

        SceneManager.UnloadSceneAsync(0); // Unload Initialisation scene after finish loading the next scene. this is the only scene in Buildsetting, thus has index 0
    }
}
