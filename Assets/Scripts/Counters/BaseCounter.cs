using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
   
    [SerializeField] private Transform spawnPoint;
   
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.Log("Interact");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("Interact diferent");
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return spawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

    
