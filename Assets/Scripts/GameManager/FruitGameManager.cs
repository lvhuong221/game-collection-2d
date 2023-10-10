using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitGameManager : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;
    [SerializeField] private VoidEventChannelSO pauseEvent;
    [SerializeField] private VoidEventChannelSO resumeEvent;
    private bool _isPaused;

    private void Start()
    {
        _inputReader.EnableShootToMoveInput();
        Time.timeScale = 1.0f;
    }


    private void OnEnable()
    {
        _inputReader.pauseEvent += OnPauseInput;
        resumeEvent.OnEventRaised += OnResumeEvent;
    }


    private void OnDisable()
    {
        _inputReader.pauseEvent -= OnPauseInput;
        resumeEvent.OnEventRaised -= OnResumeEvent;
    }
    private void OnPauseInput()
    {
        TogglePause();
    }

    private void OnResumeEvent()
    {
        if (_isPaused)
        {
            TogglePause();
        }
    }
    private void TogglePause()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        pauseEvent.Raise();
    }
}
