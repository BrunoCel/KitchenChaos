using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO KitchenObjectSO;
   [SerializeField] private Transform spawnPoint;
   [SerializeField] private ClearCounter secondClearCounter;
   [SerializeField] private bool test;
   
   private KitchenObject kitchenObject;

   private void Update()
   {
      if (test && Input.GetKeyDown(KeyCode.T))
      {
         if (kitchenObject != null)
         {
            kitchenObject.SetClearCounter(secondClearCounter);
            Debug.Log(kitchenObject.GetClearCounter().name);
         }
      }
   }

   public void Interact()
   {
      if (kitchenObject == null)
      {
         Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, spawnPoint);
         kitchenObjectTransform.localPosition = Vector3.zero;

         kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
         kitchenObject.SetClearCounter(this);
         
      }
      else
      {
         Debug.Log(kitchenObject.GetClearCounter() + "is full");
      }
   }
}
