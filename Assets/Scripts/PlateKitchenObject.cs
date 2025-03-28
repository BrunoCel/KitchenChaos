using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;

    public class OnIngridientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField]private List<KitchenObjectSO> kitchenObjects;
    private List<KitchenObjectSO> kitchenObjectsList;

    void Awake()
    {
        kitchenObjectsList = new List<KitchenObjectSO>();
    }
    
    public bool TryAddIngridient(KitchenObjectSO ingredient)
    {
        if (!kitchenObjects.Contains(ingredient))
        {
            Debug.LogWarning("Ingrediente não encontrado");
            return false;
        }
        if (kitchenObjectsList.Contains(ingredient)) 
        {
                Debug.Log("KitchenObject already contains ingredient");
                return false;
                
        }
        else
        {
                kitchenObjectsList.Add(ingredient);
                OnIngridientAdded?.Invoke(this , new OnIngridientAddedEventArgs { kitchenObjectSO = ingredient });
                Debug.Log("Ingrediente adicionado");
                return true;
        }
        
    }
    
    public List<KitchenObjectSO> GetKitchenObjectsList()
    {
        return kitchenObjectsList;
    }
}
