using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    [SerializeField] private PongInputReader _inputReader;
    [SerializeField] private PongBall pongBall;
    [SerializeField] private IntVariable playerOneScoreVariable;
    [SerializeField] private IntVariable playerTwoScoreVariable;
    [Header("Events")]
    [SerializeField] private GameEvent playerOneScoreEvent;
    [SerializeField] private GameEvent playerTwoScoreEvent;
    [SerializeField] private BoolEventChannelSO pauseEvent;
    [SerializeField] private VoidEventChannelSO resumeEvent;
    [SerializeField] private PongGameModeChannelSO selectGameModeEvent;
    [SerializeField] private VoidEventChannelSO resetScoreEvent;

    private float resetCountDown = 1f;
    private int playerOneScore = 0, playerTwoScore = 0;

    private bool _isPaused;

    private void OnEnable()
    {
        playerOneScoreEvent?.AddListener(OnPlayerOneScore);
        playerTwoScoreEvent?.AddListener(OnPlayerTwoScore);
        _inputReader.PauseEvent += InputReader_PauseEvent;
        resumeEvent.OnEventRaised += OnResumeEvent;
        selectGameModeEvent.OnSelectGameMode += OnSelectGameMode;
        resetScoreEvent.OnEventRaised += ResetGame;
    }

    private void OnDisable()
    {
        playerOneScoreEvent?.RemoveListener(OnPlayerOneScore);
        playerTwoScoreEvent?.RemoveListener(OnPlayerTwoScore);
        _inputReader.PauseEvent -= InputReader_PauseEvent;
        resumeEvent.OnEventRaised -= OnResumeEvent;
        selectGameModeEvent.OnSelectGameMode -= OnSelectGameMode;
        resetScoreEvent.OnEventRaised -= ResetGame;
    }

    private void Start()
    {
        _inputReader.DisableAllInput();

        playerOneScoreVariable.Value = 0;
        playerTwoScoreVariable.Value = 0;
    }

    private void OnResumeEvent()
    {
        if(_isPaused)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        pauseEvent.Raise(_isPaused);
    }

    private void InputReader_PauseEvent()
    {
        TogglePause();
    }
    

    private void ResetGame()
    {
        _isPaused = false;
        Time.timeScale = 1;

        playerOneScoreVariable.Value = 0;
        playerTwoScoreVariable.Value = 0;
        pongBall.Reset();
        _inputReader.EnableGameplayInput();
    }

    private void OnSelectGameMode(PongGameModeChannelSO.PongGameMode arg0)
    {
        // Set player 2 to bot or player 2 depend on game mode
        // Or listen to even on playTwo object?
        ResetGame();
    }

    private void OnPlayerOneScore()
    {
        playerOneScore++;
        playerOneScoreVariable.Value = playerOneScore;
        RestartRound();
    }

    private void OnPlayerTwoScore()
    {
        playerTwoScore++;
        playerTwoScoreVariable.Value = playerTwoScore;
        RestartRound();
    }

    private void RestartRound()
    {
        pongBall.gameObject.SetActive(false);
        Invoke("ResetBall", resetCountDown);
    }

    private void ResetBall()
    {
        pongBall.gameObject.SetActive(true);
        pongBall.Reset();
    }

}
