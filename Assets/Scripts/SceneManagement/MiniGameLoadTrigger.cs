using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script load 
/// </summary>
public class MiniGameLoadTrigger : MonoBehaviour
{
    [SerializeField] MiniGameSO _miniGameToLoad = default;
    [SerializeField] LoadSceneEventChannelSO _loadMiniGameChannel;
    [SerializeField] Button _triggerButton;

    private void OnEnable()
    {
        _triggerButton.onClick.AddListener(LoadMiniGame);
    }

    private void LoadMiniGame()
    {
        _loadMiniGameChannel?.Raise(_miniGameToLoad, true);
    }
}
