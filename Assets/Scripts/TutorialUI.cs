using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    public static TutorialUI Instance { get; private set; }
    private void Awake() {
        Instance = this;

    }
    private void Start()
    {
        KitchenGameManager.Instance.OnStageChanged += KitchenGameManager_OnStageChanged;
        Show();
    }

    private void KitchenGameManager_OnStageChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }


}
