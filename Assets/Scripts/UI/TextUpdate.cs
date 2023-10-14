using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreEvent;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private string prefix;

    private void OnEnable()
    {
        scoreEvent.OnEventRaised += UpdateScore;
    }

    private void OnDisable()
    {
        scoreEvent.OnEventRaised -= UpdateScore;
    }

    private void UpdateScore(int value)
    {
        scoreText.text = prefix + value;
    }
}
