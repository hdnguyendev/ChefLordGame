using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    private void Start()
    {
        KitchenGameManager.Instance.OnStageChanged += KitchenGameManager_OnStageChanged;
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        Hide();
    }

    private void OnMainMenuButtonClicked()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void OnPlayAgainButtonClicked()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void KitchenGameManager_OnStageChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
            playerScoreText.text = DeliveryManager.Instance.GetPlayerScore().ToString();
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
}
