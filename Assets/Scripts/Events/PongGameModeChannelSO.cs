using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for broadcasting pong game mode
/// </summary>
[CreateAssetMenu(menuName = "Events/Pong game mode Event Channel")]
public class PongGameModeChannelSO : DescriptionBaseSO
{
    public enum PongGameMode
    {
        PvE,
        PvP
    }

    public UnityAction<PongGameMode> OnSelectGameMode;

    public void Raise(PongGameMode gameMode)
    {
        if (OnSelectGameMode != null)
        {
            OnSelectGameMode.Invoke(gameMode);
        } else
        {
            Debug.LogWarning("A Select pong game mode event was requested, but nobody picked it up. " +
                "This should be listened to by PongGameManager, ");
        }
    }
}
