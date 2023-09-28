using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    [SerializeField] PongBall pongBall;
    [SerializeField] GameEvent playerOneScoreEvent;
    [SerializeField] GameEvent playerTwoScoreEvent;
    [SerializeField] IntVariable playerOneScoreVariable;
    [SerializeField] IntVariable playerTwoScoreVariable;
    [SerializeField] BoolEventChannelSO pauseEvent;
    [SerializeField] PongGameModeChannelSO selectGameModeEvent;

    private float resetCountDown = 1f;
    private int playerOneScore = 0, playerTwoScore = 0;

    private bool _isPaused;

    private void OnEnable()
    {
        playerOneScoreEvent?.AddListener(OnPlayerOneScore);
        playerTwoScoreEvent?.AddListener(OnPlayerTwoScore);
        _inputReader.PauseEvent += InputReader_PauseEvent;
        selectGameModeEvent.OnSelectGameMode += OnSelectGameMode;
    }

    private void OnDisable()
    {
        playerOneScoreEvent?.RemoveListener(OnPlayerOneScore);
        playerTwoScoreEvent?.RemoveListener(OnPlayerTwoScore);
        _inputReader.PauseEvent -= InputReader_PauseEvent;
        selectGameModeEvent.OnSelectGameMode -= OnSelectGameMode;
    }

    private void Start()
    {
        _inputReader.DisableAllInput();
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
    

    private void StartGame()
    {
        _isPaused = false;
        Time.timeScale = 1;

        pongBall.Reset();
        _inputReader.EnableGameplayInput();
    }

    private void OnSelectGameMode(PongGameModeChannelSO.PongGameMode arg0)
    {
        // Set player 2 to bot or player 2 depend on game mode
        // Or listen to even on playTwo object?
        StartGame();
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
