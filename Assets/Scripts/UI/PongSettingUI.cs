using System;
using UnityEngine;
using UnityEngine.UI;

public class PongSettingUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button selectGameModeButton;
    [SerializeField] private Button resetScoreButton;
    [SerializeField] private Button returnToMenuButton;

    [SerializeField] private VoidEventChannelSO resumeEvent;
    [SerializeField] private VoidEventChannelSO clickSelectGameModeEvent;
    [SerializeField] private VoidEventChannelSO resetScoreEvent;
    [SerializeField] private LoadSceneEventChannelSO loadMenuEvent;
    [SerializeField] private GameSceneSO menuScene;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnClickResume);
        selectGameModeButton.onClick.AddListener(OnClickSelectGameMode);
        resetScoreButton.onClick.AddListener(OnClickResetScore);
        returnToMenuButton.onClick.AddListener(OnClickReturnToMenu);
    }
    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnClickResume);
        selectGameModeButton.onClick.RemoveListener(OnClickSelectGameMode);
        resetScoreButton.onClick.RemoveListener(OnClickResetScore);
        returnToMenuButton.onClick.RemoveListener(OnClickReturnToMenu);

    }

    private void OnClickResume()
    {
        gameObject.SetActive(false);
        resumeEvent.Raise();
    }

    private void OnClickSelectGameMode()
    {
        gameObject.SetActive(false);
        clickSelectGameModeEvent.Raise();
    }

    private void OnClickResetScore()
    {
        gameObject.SetActive(false);
        resetScoreEvent.Raise();
    }

    private void OnClickReturnToMenu()
    {
        loadMenuEvent.Raise(menuScene, true, true);
    }

}
