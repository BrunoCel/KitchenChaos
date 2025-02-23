using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
   [SerializeField] private KitchenObjectSO KitchenObjectSO;
   [SerializeField] private Transform spawnPoint;
   
   private KitchenObject kitchenObject;

   private void Update()
   {
      
   }

   public void Interact(Player player)
   {
      if (kitchenObject == null)
      {
         Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, spawnPoint);
         kitchenObjectTransform.GetComponent<KitchenObject>().SetKitcheObjectParent(this);
         
      }
      else
      {
        kitchenObject.SetKitcheObjectParent(player);
      }
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
