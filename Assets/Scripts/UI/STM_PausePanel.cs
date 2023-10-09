using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STM_PausePanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnToMenuButton;

    [SerializeField] private VoidEventChannelSO resumeEvent;
    [SerializeField] private LoadSceneEventChannelSO loadMenuEvent;
    [SerializeField] private GameSceneSO menuScene;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnClickResume);
        returnToMenuButton.onClick.AddListener(OnClickReturnToMenu);
    }
    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnClickResume);
        returnToMenuButton.onClick.RemoveListener(OnClickReturnToMenu);

    }

    private void OnClickResume()
    {
        resumeEvent.Raise();
    }

    private void OnClickReturnToMenu()
    {
        loadMenuEvent.Raise(menuScene, true, true);
    }

}
