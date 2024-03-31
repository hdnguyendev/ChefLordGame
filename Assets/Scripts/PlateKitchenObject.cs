using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    // List cac vat pham tren dia
    private List<KitchenObjectSO> kitchenObjectSOList;

    public void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    // Them vat pham vao dia
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // kiem tra xem vat pham co nam trong danh sach vat pham hop le khong
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // khong hop le
            return false;
        }
            
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Da co vat pham nay tren dia
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
