using Cinemachine;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerObject : MonoBehaviour
{
    [SerializeField] private GameObjectVariable playerVariable;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void OnEnable()
    {
        playerVariable.AddListener(UpdateTarget);
    }

    private void OnDisable()
    {
        playerVariable.RemoveListener(UpdateTarget);
    }

    private void UpdateTarget()
    {
        if (playerVariable != null)
        {
            virtualCamera.Follow = playerVariable.Value.transform;
        }
    }
}
