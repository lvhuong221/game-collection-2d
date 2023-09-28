using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] BoolEventChannelSO loadingScreenEvent;
    [SerializeField] GameObject loadingScreenImage;
    [SerializeField] RectTransform loadingIcon;

    private void OnEnable()
    {
        loadingScreenEvent.OnEventRaised += LoadingScreenEvent_OnEventRaised;
    }

    private void OnDisable()
    {
        loadingScreenEvent.OnEventRaised -= LoadingScreenEvent_OnEventRaised;
    }

    private void Start()
    {
        loadingIcon.DOLocalRotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    }

    private void LoadingScreenEvent_OnEventRaised(bool value)
    {
        loadingScreenImage.SetActive(value);
    }
}
