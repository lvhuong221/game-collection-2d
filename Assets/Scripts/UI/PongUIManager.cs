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
    [SerializeField] BoolEventChannelSO pauseEvent;
    [SerializeField] AssetReferenceGameObject settingUI;

    private GameObject settingUIGameObject;

    private void Start()
    {
        playerOneScoreVariable?.AddListener(UpdatePlayerScores);
        playerTwoScoreVariable?.AddListener(UpdatePlayerScores);
        pauseEvent.OnEventRaised += PauseEvent_OnEventRaised;
    }

    private void OnDestroy()
    {
        playerOneScoreVariable?.RemoveListener(UpdatePlayerScores);
        playerTwoScoreVariable?.RemoveListener(UpdatePlayerScores);
        pauseEvent.OnEventRaised -= PauseEvent_OnEventRaised;
    }

    private void UpdatePlayerScores()
    {
        playerOneScoreText.text = "" + playerOneScoreVariable.Value;
        playerTwoScoreText.text = "" + playerTwoScoreVariable.Value;
    }


    private void PauseEvent_OnEventRaised(bool value)
    {
        if (settingUIGameObject != null)
            return;
        if(value)
        {
            settingUI.LoadAssetAsync<GameObject>().Completed += OnLoadUICompleted;
        }
        else
        {
            settingUI.ReleaseInstance(settingUIGameObject);
        }
    }

    private void OnLoadUICompleted(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            settingUIGameObject = Instantiate(asyncOperationHandle.Result, transform);
            settingUIGameObject.SetActive(true);
        } else
        {
            Debug.LogError("Fail to load addressable asset" + settingUI.ToString());
        }
    }
}
