using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class PlateCompleteVisual : MonoBehaviour
{
   
   [Serializable]public struct KitchenObjectSO_GameObject
   {
      public KitchenObjectSO kitchenObjectSO;
      public GameObject gameObject;
   }

   [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSoGameObjectsList;
   [SerializeField] private PlateKitchenObject plateKitchenObject;

   void Start()
   {
      plateKitchenObject.OnIngridientAdded+= PlateKitchenObjectOn_OnIngridientAdded;
   }
   

   void PlateKitchenObjectOn_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
   {
      foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSoGameObjectsList)
      {
         if (kitchenObjectSoGameObject.kitchenObjectSO == e.kitchenObjectSO)
         {
            kitchenObjectSoGameObject.gameObject.SetActive(true);
         }
      }
   }
}
