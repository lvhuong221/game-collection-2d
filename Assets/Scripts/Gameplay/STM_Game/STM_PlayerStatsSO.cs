using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="STM_CharacterStat", menuName ="Character/STM Stats")]
public class STM_PlayerStatsSO : DescriptionBaseSO
{
    public UnityAction OnEventRaised;

    public void Raise()
    {
        OnEventRaised.Invoke();
    }

    [Range(100, 1000)] public float jumpForce = 500;
    public int currentAmmoCount = 0; // Number depend on level
    public float shootCooldown = 0.5f; // Time between shots
}
