using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour, IHasProgress
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private RecipeSO recipeSO;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (recipeSO == null) return;
        recipeSO.timeRun += Time.deltaTime;
        if (recipeSO.timeRun < recipeSO.timeLimit)
        {
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized =  (float) recipeSO.timeRun / recipeSO.timeLimit
            });
        }
        else
        {
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 0f
            });
        }
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        this.recipeSO = recipeSO;
        recipeNameText.text = recipeSO.recipeName;
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
        
    }

}
