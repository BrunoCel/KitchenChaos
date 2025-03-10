using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class PanRecipesSO : ScriptableObject
{
    public KitchenObjectSO inputKitchenObject;
    public KitchenObjectSO outputKitchenObject;
    public KitchenObjectSO outputKitchenObjectBurned;
    
    public float TimeToBeFried;
    public float TimeToBurn;
}
