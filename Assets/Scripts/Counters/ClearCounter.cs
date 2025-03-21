using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearCounter : BaseCounter 
{
   public override void Interact(Player player)
   {
       if (!HasKitchenObject())
       {
           if (player.HasKitchenObject())
           {
               player.GetKitchenObject().SetKitcheObjectParent(this);
               Debug.Log(GetKitchenObject().GetKitchenObjectSO());
           }
       }
       else
       {

           if (!player.HasKitchenObject())
           {
               GetKitchenObject().SetKitcheObjectParent(player);
           }
           else
           {
               //player is holding a plate
               if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
               {
                   if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                   {
                       Debug.Log("Item adicionado ao prato");
                       GetKitchenObject().DestroyKitchenObject();
                   }
               }
               else if(GetKitchenObject().TryGetPlate(out PlateKitchenObject _plate))
               {
                   if (_plate.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO()))
                   {
                       player.GetKitchenObject().DestroyKitchenObject();
                   }
               }
           }
       }
   }
}


