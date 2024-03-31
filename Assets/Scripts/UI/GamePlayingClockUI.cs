using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    private void Start()
    {
        KitchenGameManager.Instance.OnStageChanged += KitchenGameManager_OnStageChanged;
        Hide();
    }

    private void KitchenGameManager_OnStageChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
          gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
        timerText.text = Mathf.CeilToInt(KitchenGameManager.Instance.GetGamePlayingTimer()).ToString();
    }
}
