using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public override void Interact(Player player)
    {
            if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
            {
                DeliveyManager.instance.DeliverRecipe(plate);
                player.GetKitchenObject().SetKitcheObjectParent(this);
                GetKitchenObject().DestroyKitchenObject();
            }
    }
}
