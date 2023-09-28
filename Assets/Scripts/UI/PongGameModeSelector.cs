using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongGameModeSelector : MonoBehaviour
{
    [SerializeField] Button pveButton;
    [SerializeField] Button pvpButton;

    [SerializeField] PongGameModeChannelSO pongGameMode;

    private void Start()
    {
        pveButton.Select();
    }
    private void OnEnable()
    {
        pvpButton.onClick.AddListener(SelectPvP);
        pveButton.onClick.AddListener(SelectPvE);
    }

    private void OnDisable()
    {
        pvpButton.onClick.RemoveListener(SelectPvP);
        pveButton.onClick.RemoveListener(SelectPvE);
    }
    private void SelectPvP()
    {
        pongGameMode.Raise(PongGameModeChannelSO.PongGameMode.PvP);
        gameObject.SetActive(false);
    }

    private void SelectPvE()
    {
        pongGameMode.Raise(PongGameModeChannelSO.PongGameMode.PvE);
        gameObject.SetActive(false);
    }

}
