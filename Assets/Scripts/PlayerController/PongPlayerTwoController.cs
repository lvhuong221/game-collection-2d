using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PongPlayerTwoController : PongPlayerMovement
{
    [SerializeField] private PongInputReader _inputReader;
    [SerializeField] private PongGameModeChannelSO selectGameModeEvent;
    [SerializeField] private PongBall pongBall;

    [SerializeField, Range(1, 10)] int difficulty = 1;

    private PongGameModeChannelSO.PongGameMode gameMode;
    private float baseReactionTime = .8f;
    float timeToReact;
    private DateTime lastReactTime;

    private void Start()
    {
        if (pongBall == null)
        {
            Debug.LogWarning("Player 2 has no reference to pong bal");
        }

        lastReactTime = DateTime.Now;
    }
    private void OnEnable()
    {
        _inputReader.PlayerTwoMoveEvent += InputReader_PlayerTwoMoveEvent;
        selectGameModeEvent.OnSelectGameMode += OnSelectGameMode;
    }

    private void OnDisable()
    {
        _inputReader.PlayerTwoMoveEvent -= InputReader_PlayerTwoMoveEvent;
        selectGameModeEvent.OnSelectGameMode -= OnSelectGameMode;
    }

    private void OnSelectGameMode(PongGameModeChannelSO.PongGameMode selectedGameMode)
    {
        gameMode = selectedGameMode;
    }

    private void InputReader_PlayerTwoMoveEvent(Vector2 arg0)
    {
        if (gameMode == PongGameModeChannelSO.PongGameMode.PvP)
        {
            UpdateMoveVector(arg0);
        }
    }

    protected override void BeforeMove()
    {
        UpdateBotMovement();
    }

    private void UpdateBotMovement()
    {
        // Putting here allow change difficulty during runtime
        // reaction time decrease inversely proportional with difficulty
        timeToReact = baseReactionTime * (1f / difficulty);
        if (gameMode == PongGameModeChannelSO.PongGameMode.PvE && pongBall != null)
        {
            //if (DateTime.Now - lastReactTime < TimeSpan.FromSeconds(timeToReact))
            //{
            //    return;
            //}
            lastReactTime = DateTime.Now;

            if (pongBall.transform.position.y - transform.position.y > 0.4f)
            {
                UpdateMoveVector(Vector2.up);
            } else if (pongBall.transform.position.y - transform.position.y < -0.4f)
            {
                UpdateMoveVector(Vector2.down);
            }
        }
    }

}
