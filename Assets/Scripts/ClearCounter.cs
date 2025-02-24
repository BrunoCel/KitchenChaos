using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearCounter : BaseCounter 
{
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
}
