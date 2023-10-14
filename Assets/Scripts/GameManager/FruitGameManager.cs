using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitGameManager : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;

    [Header("Game flow events")]
    [SerializeField] private VoidEventChannelSO pauseEvent;
    [SerializeField] private VoidEventChannelSO resumeEvent;

    [Header("Game data")] 
    [SerializeField] private IntEventChannelSO gainScoreEvent;
    [SerializeField] private IntEventChannelSO currentScoreEvent;
    [SerializeField] private IntEventChannelSO highScoreEvent;

    private bool _isPaused;

    private int _currentScore =0;
    private int _highScore =0;

    private void Start()
    {
        _inputReader.EnableShootToMoveInput();
        Time.timeScale = 1.0f;

        _highScore = PlayerPrefs.GetInt(Constants.FruitGame.HIGH_SCORE, 0);

        currentScoreEvent.Raise(0);
        highScoreEvent.Raise(_highScore);
    }


    private void OnEnable()
    {
        _inputReader.pauseEvent += OnPauseInput;
        resumeEvent.OnEventRaised += OnResumeEvent;
        gainScoreEvent.OnEventRaised += OnGainScore;
    }


    private void OnDisable()
    {
        _inputReader.pauseEvent -= OnPauseInput;
        resumeEvent.OnEventRaised -= OnResumeEvent;
        gainScoreEvent.OnEventRaised -= OnGainScore;
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

    private void OnGainScore(int value)
    {
        _currentScore += value;
        currentScoreEvent.Raise(_currentScore);
        if(_currentScore > _highScore)
        {
            _highScore = _currentScore;
            highScoreEvent.Raise(_highScore);
            PlayerPrefs.SetInt(Constants.FruitGame.HIGH_SCORE, _highScore);
            PlayerPrefs.Save();
        }
    }
}
