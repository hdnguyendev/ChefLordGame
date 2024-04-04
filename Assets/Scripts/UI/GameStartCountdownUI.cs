using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator aimator;
    private int previosCountdownNumber;

    private void Awake()
    {
        aimator = GetComponent<Animator>();
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnStageChanged += KitchenGameManager_OnStageChanged;
        Hide();
    }

    private void KitchenGameManager_OnStageChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (KitchenGameManager.Instance.IsCountdownToStartActive() && (previosCountdownNumber != countdownNumber))
        {
            SoundManager.Instance.PlayCountdownSound();
            aimator.SetTrigger(NUMBER_POPUP);
            previosCountdownNumber = countdownNumber;
            Debug.Log("Countdown number: " + countdownNumber);
        }

    }
    private void Show()
    {
        countdownText.gameObject.SetActive(true);
    }
    private void Hide()
    {
        countdownText.gameObject.SetActive(false);
    }
}
