using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PongUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text playerOneScoreText;
    [SerializeField] TMP_Text playerTwoScoreText;

    [SerializeField] IntVariable playerOneScoreVariable;
    [SerializeField] IntVariable playerTwoScoreVariable;

    private void Start()
    {
        playerOneScoreVariable?.AddListener(UpdatePlayerScores);
        playerTwoScoreVariable?.AddListener(UpdatePlayerScores);
    }

    private void OnDestroy()
    {
        playerOneScoreVariable?.RemoveListener(UpdatePlayerScores);
        playerTwoScoreVariable?.RemoveListener(UpdatePlayerScores);
    }

    private void UpdatePlayerScores()
    {
        playerOneScoreText.text = "Score: " + playerOneScoreVariable.Value;
        playerTwoScoreText.text = "Score: " + playerTwoScoreVariable.Value;
    }
}
