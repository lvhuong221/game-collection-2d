using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    [SerializeField] PongBall pongBall;
    [SerializeField] GameEvent playerOneScoreEvent;
    [SerializeField] GameEvent playerTwoScoreEvent;
    [SerializeField] IntVariable playerOneScoreVariable;
    [SerializeField] IntVariable playerTwoScoreVariable;

    private float resetCountDown = 1f;
    private int playerOneScore = 0, playerTwoScore = 0;



    private void Start()
    {
        playerOneScoreEvent?.AddListener(OnPlayerOneScore);
        playerTwoScoreEvent?.AddListener(OnPlayerTwoScore);

        pongBall.Reset();
    }

    private void OnDestroy()
    {
        playerOneScoreEvent?.RemoveListener(OnPlayerOneScore);
        playerTwoScoreEvent?.RemoveListener(OnPlayerTwoScore);
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
