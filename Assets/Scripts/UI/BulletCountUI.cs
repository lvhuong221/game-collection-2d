using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountUI : MonoBehaviour
{
    [SerializeField] private STM_PlayerStatsSO playerStat;
    [SerializeField] private TMP_Text bulletCountText;
    [SerializeField] private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerStat.OnEventRaised += OnShoot;
    }

    private void OnDisable()
    {
        playerStat.OnEventRaised -= OnShoot;
    }

    private void OnShoot()
    {
        bulletCountText.text = playerStat.currentAmmoCount + "";
        icon.fillAmount = 0;
        icon.DOFillAmount(1, playerStat.shootCooldown);
    }

}
