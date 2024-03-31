using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // There is no KitchenObject here
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        }
        else
        // There is a KitchenObject here
        {
            if (player.HasKitchenObject())
            {
                // Player is carring something 
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate 
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not holding a Plate but is carrying something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) { 
                            player.GetKitchenObject().DestroySelf();
                        }
                    }

                }
            }
            else
            {
                // Player is not carry anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
