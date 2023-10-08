using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private PongInputReader _inputReader;

    [Header("Listen to")]
    [SerializeField] private LoadSceneEventChannelSO _loadMenuEvent;
    [SerializeField] private LoadSceneEventChannelSO _miniGameEvent;
    [SerializeField] private LoadSceneEventChannelSO _loadGameplayExtraEvent;
    [SerializeField] private LoadSceneEventChannelSO _unloadSceneEvent;

    [Header("Broadcast")]
    [SerializeField] private BoolEventChannelSO _toggleLoadingScreen = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private FadeEventChannelSO _fadeRequestChannel = default;

    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

    private GameSceneSO _sceneToLoad;
    private GameSceneSO _currentlyLoadedScene;

    private GameSceneSO _gameplaySceneToLoad;
    private GameSceneSO _loadedGameplayScene;

    private bool _fadeScreen;
    private float _fadeDuration = .5f;
    private bool _showLoadingScreen;
    private bool _isLoading; // prevent new loading request while loading a new scene

    private void OnEnable()
    {
        _loadMenuEvent.OnLoadingRequested += LoadMenu;
        _miniGameEvent.OnLoadingRequested += LoadMiniGame;
        _loadGameplayExtraEvent.OnLoadingRequested += LoadGameplayManager;
        _unloadSceneEvent.OnLoadingRequested += UnloadScene;
    }
    private void OnDisable()
    {
        _loadMenuEvent.OnLoadingRequested -= LoadMenu;
        _miniGameEvent.OnLoadingRequested -= LoadMiniGame;
        _loadGameplayExtraEvent.OnLoadingRequested -= LoadGameplayManager;
        _unloadSceneEvent.OnLoadingRequested -= UnloadScene;
    }

    private void LoadGameplayManager(GameSceneSO sceneToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (_isLoading)
            return;

        _gameplaySceneToLoad = sceneToLoad;
        _showLoadingScreen = showLoadingScreen;
        _fadeScreen = fadeScreen;

        LoadGameplayScene();
    }

    private void LoadMenu(GameSceneSO sceneToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (_isLoading)
            return;

        _sceneToLoad = sceneToLoad;
        _showLoadingScreen = showLoadingScreen;
        _fadeScreen = fadeScreen;

        StartCoroutine("UnloadPreviousScene");
        // If go back to menu from gameplay scene
        StartCoroutine("UnloadGameplayScene");
    }

    private void LoadMiniGame(GameSceneSO sceneToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (_isLoading)
            return;

        _sceneToLoad = sceneToLoad;
        _showLoadingScreen = showLoadingScreen;
        _fadeScreen = fadeScreen;

        StartCoroutine("UnloadPreviousScene");
    }

    private void UnloadScene(GameSceneSO sceneToUnload, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (sceneToUnload.sceneReference.OperationHandle.IsValid())
        {
            sceneToUnload.sceneReference.UnLoadScene();
        }
    }

    private IEnumerator UnloadPreviousScene()
    {
        // each game will have its own game manager and will enable input from there
        _inputReader.DisableAllInput();
        if (_fadeScreen)
        {
            _fadeRequestChannel.FadeOut(_fadeDuration);
        }

        yield return new WaitForSecondsRealtime(_fadeDuration);

        if(_currentlyLoadedScene != null) // null if player start from Initialsation
        {
            if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
            {
                _currentlyLoadedScene.sceneReference.UnLoadScene();
            }
#if UNITY_EDITOR
            else
            {
                //Only used when, after a "cold start", the player moves to a new scene
                //Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
                //the scene needs to be unloaded using regular SceneManager instead of as an Addressable
                SceneManager.UnloadSceneAsync(_currentlyLoadedScene.sceneReference.editorAsset.name);
            }
#endif
        }

        LoadNewScene();
    }

    private IEnumerator UnloadGameplayScene()
    {
        yield return new WaitForSecondsRealtime(_fadeDuration);
        if(_loadedGameplayScene != null)
        {
            if (_loadedGameplayScene.sceneReference.OperationHandle.IsValid())
            {
                _loadedGameplayScene.sceneReference.UnLoadScene();
            }
        }
    }

    private void LoadNewScene()
    {
        if (_showLoadingScreen)
        {
            _toggleLoadingScreen.Raise(true);
        }

        _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }
    private void LoadGameplayScene()
    {
        if (_showLoadingScreen)
        {
            _toggleLoadingScreen.Raise(true);
        }

        _gameplayManagerLoadingOpHandle = _gameplaySceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
        _gameplayManagerLoadingOpHandle.Completed += OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        // save loaded scene to be unloaded at next request
        _currentlyLoadedScene = _sceneToLoad;

        Scene s = obj.Result.Scene;
        SceneManager.SetActiveScene(s);
        LightProbes.TetrahedralizeAsync();

        _isLoading = false;

        _toggleLoadingScreen.Raise(false);
        _fadeRequestChannel.FadeIn(_fadeDuration);

        FinishLoading();
    }

    private void OnGUI()
    {
        
    }

    private void FinishLoading() 
    {
        _onSceneReady.Raise();
    }
}
