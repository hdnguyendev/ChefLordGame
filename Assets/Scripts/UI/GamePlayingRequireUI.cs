using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayingRequireUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI requireScoreText;
     private void Start()
    {
        KitchenGameManager.Instance.OnStageChanged += KitchenGameManager_OnStageChanged;


        requireScoreText.text = DeliveryManager.Instance.GetScoreCompletedLevel().ToString();
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

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Update()
    {
        playerScoreText.text = DeliveryManager.Instance.GetPlayerScore().ToString();
    }
}
