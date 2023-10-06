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
    [SerializeField] VoidEventChannelSO openSelectGameModeEvent;

    private void Start()
    {
        pveButton.Select();
        openSelectGameModeEvent.OnEventRaised += OnOpenSelectGameModeEvent;
    }

    private void OnDestroy()
    {
        openSelectGameModeEvent.OnEventRaised -= OnOpenSelectGameModeEvent;
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

    private void OnOpenSelectGameModeEvent()
    {
        gameObject.SetActive(true);
    }
}
