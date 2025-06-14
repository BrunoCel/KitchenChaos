using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrashCounter : BaseCounter
{
    public static event EventHandler OnDiscard;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitcheObjectParent(this);
                OnDiscard?.Invoke(this, EventArgs.Empty);
                Invoke("DiscardKitchenObject", 1f);
            }
        }
        
    }

    public void DiscardKitchenObject()
    {
        GetKitchenObject().DestroyKitchenObject();
    }

   new public static void ClearStaticData()
    {
        OnDiscard = null;
    }
}
