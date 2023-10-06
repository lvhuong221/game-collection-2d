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
    [SerializeField] private InputReader _inputReader;

    [Header("Listen to")]
    [SerializeField] private LoadSceneEventChannelSO _loadMenuEvent;
    [SerializeField] private LoadSceneEventChannelSO _miniGameEvent;

    [Header("Broadcast")]
    [SerializeField] private BoolEventChannelSO _toggleLoadingScreen = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private FadeEventChannelSO _fadeRequestChannel = default;

    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

    private GameSceneSO _sceneToLoad;
    private GameSceneSO _currentlyLoadedScene;

    private bool _fadeScreen;
    private float _fadeDuration = .5f;
    private bool _showLoadingScreen;
    private bool _isLoading; // prevent new loading request while loading a new scene

    private void OnEnable()
    {
        _loadMenuEvent.OnLoadingRequested += LoadMenu;
        _miniGameEvent.OnLoadingRequested += LoadMiniGame;
    }
    private void OnDisable()
    {
        _loadMenuEvent.OnLoadingRequested -= LoadMenu;
        _miniGameEvent.OnLoadingRequested -= LoadMiniGame;
    }


    private void LoadMenu(GameSceneSO sceneToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (_isLoading)
            return;

        _sceneToLoad = sceneToLoad;
        _showLoadingScreen = showLoadingScreen;
        _fadeScreen = fadeScreen;

        StartCoroutine("UnloadPreviousScene");
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

    private IEnumerator UnloadPreviousScene()
    {
        // each game will have its own game manager and will enable input from there
        _inputReader.DisableAllInput();

        yield return new WaitForSecondsRealtime(_fadeDuration);

        _fadeRequestChannel.FadeOut(_fadeDuration);
        if (_currentlyLoadedScene != null) // null if player start from Initialsation
        {
            if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
            {
                _currentlyLoadedScene.sceneReference.UnLoadScene();
            }
        }

        LoadNewScene();
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

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        // save loaded scene to be unloaded at next request
        _currentlyLoadedScene = _sceneToLoad;

        Scene s = obj.Result.Scene;
        SceneManager.SetActiveScene(s);
        LightProbes.TetrahedralizeAsync();

        _isLoading = false;

        if(_showLoadingScreen)
        {
            _toggleLoadingScreen.Raise(false);
        }

        _fadeRequestChannel.FadeIn(_fadeDuration);

        FinishLoading();
    }

    private void FinishLoading() 
    {
        _onSceneReady.Raise();
    }
}
