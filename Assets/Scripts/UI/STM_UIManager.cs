using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class STM_UIManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private VoidEventChannelSO pauseEvent;
    [SerializeField] private VoidEventChannelSO resumeEvent;

    [Header("UI elements")]
    //[SerializeField] private Button tutorialImage;
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        //tutorialImage.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        pauseEvent.OnEventRaised += PauseEvent_OnEventRaised;
        resumeEvent.OnEventRaised += OnResume;
    }


    private void OnDisable()
    {
        pauseEvent.OnEventRaised -= PauseEvent_OnEventRaised;
        resumeEvent.OnEventRaised -= OnResume;
    }

    private void PauseEvent_OnEventRaised()
    {
        pausePanel.SetActive(true);
    }
    private void OnResume()
    {
        pausePanel.SetActive(false);
    }
}
