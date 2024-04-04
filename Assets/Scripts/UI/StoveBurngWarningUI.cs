using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurngWarningUI : MonoBehaviour
{
   [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProcessAmount = .5f;
        bool show = stoveCounter.isFired() && e.progressNormalized >= burnShowProcessAmount;

        if (show) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
