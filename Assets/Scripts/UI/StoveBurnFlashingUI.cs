using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProcessAmount = .5f;
        bool show = stoveCounter.isFired() && e.progressNormalized >= burnShowProcessAmount;

       animator.SetBool(IS_FLASHING, show);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}

