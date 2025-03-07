using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

   [SerializeField] private KitchenObjectSO cuttingObject;
   public override void Interact(Player player)
   {
      
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            player.GetKitchenObject().SetKitcheObjectParent(this);
         }
      }
      else
      {
         if (!player.HasKitchenObject())
         {
            GetKitchenObject().SetKitcheObjectParent(player);
         }
      }
      
   }

   public override void InteractAlternate(Player player)
   {
      if (HasKitchenObject())
      {
         GetKitchenObject().DestroyKitchenObject();

         KitchenObject.SpawnKitchenObject(cuttingObject, this);

      }
      
   }
}
