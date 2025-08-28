using System;
using UnityEngine;

public class StoveCounter : BaseCounter , IHasProgress
{
   public event EventHandler<IHasProgress.OnProgressChangedArgs> OnProgressChanged;
   public event EventHandler<OnStateChangedArgs> OnStateChanged;

   public class OnStateChangedArgs : EventArgs
   {
      public State state;
   }
   
   public enum State
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
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = fryingTimer / panRecipesSo.TimeToBeFried  });
                if (fryingTimer >= panRecipesSo.TimeToBeFried)
                {
                   GetKitchenObject().DestroyKitchenObject();

                   KitchenObject.SpawnKitchenObject(panRecipesSo.outputKitchenObject, this);
                   
                   state = State.Fried;
                   OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
                   burningTimer = 0f;
                }

                break;
             case State.Fried:
                burningTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = burningTimer / panRecipesSo.TimeToBurn  });
                if (burningTimer >= panRecipesSo.TimeToBurn)
                {
                   GetKitchenObject().DestroyKitchenObject();
                   KitchenObject.SpawnKitchenObject(panRecipesSo.outputKitchenObjectBurned, this);
                   state = State.Burned;
                   OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
                }
                
                break;
             case State.Burned:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = burningTimer / panRecipesSo.TimeToBurn });
                    state = State.Burned;
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
               
               player.GetKitchenObject().SetKitcheObjectParent(this);
               
               panRecipesSo = GetPanRecipeSO(GetKitchenObject().GetKitchenObjectSO());

               state = State.Frying;
               OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
               fryingTimer = 0;
               OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = fryingTimer / panRecipesSo.TimeToBeFried  });

            }
         }
      }
      else
      {
         if (!player.HasKitchenObject())
         {
           // PanRecipesSO cuttingRecipe = GetPanRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            
            GetKitchenObject().SetKitcheObjectParent(player);
            state = State.Idle;
            OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
            fryingTimer = 0;
            burningTimer = 0;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = fryingTimer / panRecipesSo.TimeToBeFried  });
         }
         else
         {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
               if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
               {
                  Debug.Log("Item adicionado ao prato");
                  GetKitchenObject().DestroyKitchenObject();
                  state = State.Idle;
                  OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
                  fryingTimer = 0;
                  burningTimer = 0;
                  OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs() { progressNormalized = fryingTimer / panRecipesSo.TimeToBeFried  });
               }
            }
            
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
   
    public bool IsFried()
    {
        return state == State.Fried;
    }

}
