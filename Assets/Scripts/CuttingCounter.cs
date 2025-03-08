using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

   [SerializeField] private CuttingRecipeSO[] cuttingObjectArray;
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

         KitchenObjectSO outPutKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
         GetKitchenObject().DestroyKitchenObject();

         KitchenObject.SpawnKitchenObject(outPutKitchenObject, this);

      }
      
   }

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
   {
      foreach (CuttingRecipeSO cuttingRecipeSo in cuttingObjectArray)
      {
         if (cuttingRecipeSo.inputKitchenObject == inputKitchenObjectSo)
         {
            return cuttingRecipeSo.outputKitchenObject;
         }
      }
      
      return null;
   }
}
