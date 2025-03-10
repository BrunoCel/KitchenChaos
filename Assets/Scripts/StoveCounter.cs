using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

   public event EventHandler PutIn;
   public event EventHandler PutOff;
   private enum State
   {
      Idle,
      Frying,
      Fried,
      Burned,
   }

   private State state;
   
   
    [SerializeField] private PanRecipesSO[] PanObjectArray;

    private float fryingTimer = 0;
    private float burningTimer = 0;
    private PanRecipesSO panRecipesSo;

    void Start()
    {
       state = State.Idle;
    }
    private void Update()
    {
       if (HasKitchenObject())
       {
          switch (state)
          {
             case State.Idle:
                
                break;
             case State.Frying:
                fryingTimer += Time.deltaTime;
                if (fryingTimer >= panRecipesSo.TimeToBeFried)
                {
                   GetKitchenObject().DestroyKitchenObject();

                   KitchenObject.SpawnKitchenObject(panRecipesSo.outputKitchenObject, this);
                   state = State.Fried;
                   burningTimer = 0f;
                }

                break;
             case State.Fried:
                burningTimer += Time.deltaTime;
                if (burningTimer >= panRecipesSo.TimeToBurn)
                {
                   GetKitchenObject().DestroyKitchenObject();
                   KitchenObject.SpawnKitchenObject(panRecipesSo.outputKitchenObjectBurned, this);
                   state = State.Burned;
                }
                
                break;
             case State.Burned:
                break;

          }
       }
    }
    public override void Interact(Player player)
   {
      
      if (!HasKitchenObject())
         //there is no kitcheObject in this counter
      {
         if (player.HasKitchenObject())
            //player is carry something
         {
            if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
               //player is carry something that could be fried
            {
               PutIn?.Invoke(this, EventArgs.Empty);
               player.GetKitchenObject().SetKitcheObjectParent(this);
               
               panRecipesSo = GetPanRecipeSO(GetKitchenObject().GetKitchenObjectSO());

               state = State.Frying;
               fryingTimer = 0;

            }
         }
      }
      else
      {
         if (!player.HasKitchenObject())
         {
           // PanRecipesSO cuttingRecipe = GetPanRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            PutOff?.Invoke(this, EventArgs.Empty);
            GetKitchenObject().SetKitcheObjectParent(player);
            state = State.Idle;
            fryingTimer = 0;
            burningTimer = 0;
         }
      }
      
      
   }
    
    
   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
   {
      foreach (PanRecipesSO panRecipeSo in PanObjectArray)
      {
         if (panRecipeSo.inputKitchenObject == inputKitchenObjectSo)
         {
            return panRecipeSo.outputKitchenObject;
         }
      }
      
      return null;
   }

   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
   {
      return GetOutputForInput(inputKitchenObjectSo) != null;
   }

   private PanRecipesSO GetPanRecipeSO(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (PanRecipesSO _panRecipeSo in PanObjectArray)
      {
         if (_panRecipeSo.inputKitchenObject == inputKitchenObjectSO)
         {
            return _panRecipeSo;
         }
      }
      return null;
   }
   

}
