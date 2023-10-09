using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PongUIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] TMP_Text playerOneScoreText;
    [SerializeField] TMP_Text playerTwoScoreText;

    [SerializeField] IntVariable playerOneScoreVariable;
    [SerializeField] IntVariable playerTwoScoreVariable;

    [Header("Setting")]
    [SerializeField] VoidEventChannelSO pauseEvent;
    [SerializeField] VoidEventChannelSO resumeEvent;
    [SerializeField] AssetReferenceGameObject settingUI;

    private GameObject settingUIGameObject;
    private bool startedLoading;

    private void Start()
    {
        playerOneScoreVariable?.AddListener(UpdatePlayerScores);
        playerTwoScoreVariable?.AddListener(UpdatePlayerScores);
        pauseEvent.OnEventRaised += OnPauseEvent;
        resumeEvent.OnEventRaised += OnResumeEvent;
    }

    private void OnDestroy()
    {
        playerOneScoreVariable?.RemoveListener(UpdatePlayerScores);
        playerTwoScoreVariable?.RemoveListener(UpdatePlayerScores);
        pauseEvent.OnEventRaised -= OnPauseEvent;
        resumeEvent.OnEventRaised -= OnResumeEvent;
    }

    private void UpdatePlayerScores()
    {
        playerOneScoreText.text = "" + playerOneScoreVariable.Value;
        playerTwoScoreText.text = "" + playerTwoScoreVariable.Value;
    }


    private void OnPauseEvent()
    {
        if (startedLoading)
            return;

        // Player can pause multiple times
        // we load asset on the first time and keep the object until player quit mini game
        if (settingUIGameObject == null)
        {
            settingUI.LoadAssetAsync<GameObject>().Completed += OnLoadUICompleted;
            startedLoading = true;
        } else
        {
            settingUIGameObject.SetActive(true);
        }
    }

    private void OnResumeEvent()
    {
        if (settingUIGameObject != null)
        {
            // Only toggle when already loaded asset
            settingUIGameObject.SetActive(false);
        }
    }

    private void OnLoadUICompleted(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            //settingUIGameObject = asyncOperationHandle.Result;
            Addressables.InstantiateAsync(settingUI, transform).Completed += InstantiateUICompleted;
        } else
        {
            Debug.LogError("Fail to load addressable asset" + settingUI.ToString());
        }
        startedLoading = false;
    }

    private void InstantiateUICompleted(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            settingUIGameObject = obj.Result;
            settingUIGameObject.SetActive(true);
            settingUIGameObject.transform.SetParent(transform);
        } else
        {
            Debug.LogError("Fail to instantiate addressable asset" + settingUI.ToString());
        }
    }
}
