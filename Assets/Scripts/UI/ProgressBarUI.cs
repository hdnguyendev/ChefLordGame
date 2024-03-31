using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressBarGameObject;
    [SerializeField] Image barImage;
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressBarGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressBarGameObject.name + " does not have a component that implements IHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

   
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            Hide();
        } else {
            Show();
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
}
