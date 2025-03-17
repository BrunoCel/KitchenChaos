using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CuttingCounter : BaseCounter,IHasProgress
{
   public event EventHandler<IHasProgress.OnProgressChangedArgs> OnProgressChanged;
   

   public event EventHandler IsCutting ;
   public event EventHandler PlayerGrabbedKitchenObject;
   
   [SerializeField] private CuttingRecipeSO[] cuttingObjectArray;
   private int cuttingCounter = 0;
   
   
   
   public override void Interact(Player player)
   {
      
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
               player.GetKitchenObject().SetKitcheObjectParent(this);
               CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
               OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = (float)cuttingCounter / cuttingRecipe.cuttingToBeSliced  });
               
            }
         }
      }
      else
      {
         if (!player.HasKitchenObject())
         {
            CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().SetKitcheObjectParent(player);
            cuttingCounter = 0;
            PlayerGrabbedKitchenObject?.Invoke(this, EventArgs.Empty);
         }
         else
         {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
               if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
               {
                  Debug.Log("Item adicionado ao prato");
                  GetKitchenObject().DestroyKitchenObject();
               }
            }
               
         }
      }
      
   }

   public override void InteractAlternate(Player player)
   {
      if (HasKitchenObject())
      {
         if (HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
         {
            cuttingCounter++;
            CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = (float)cuttingCounter / cuttingRecipe.cuttingToBeSliced  });
            IsCutting?.Invoke(this, new EventArgs());

            if (cuttingCounter >= GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingToBeSliced)
            {
               KitchenObjectSO outPutKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
               GetKitchenObject().DestroyKitchenObject();

               KitchenObject.SpawnKitchenObject(outPutKitchenObject, this);
            }
         }
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

   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
   {
      return GetOutputForInput(inputKitchenObjectSo) != null;
   }

   private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (CuttingRecipeSO cuttingRecipeSo in cuttingObjectArray)
      {
         if (cuttingRecipeSo.inputKitchenObject == inputKitchenObjectSO)
         {
            return cuttingRecipeSo;
         }
      }
      return null;
   }
   
}
