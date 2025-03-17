using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObjectSO;
    }

    public void SetKitcheObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        this.kitchenObjectParent = kitchenObjectParent;

        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("clear counter already has KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetClearIKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroyKitchenObject()
    {
        kitchenObjectParent.ClearKitchenObject();
        
        
        Destroy(gameObject);
        Debug.Log("destroyed KitchenObject");
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,
        IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitcheObjectParent(kitchenObjectParent);
        
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plate)
    {
        if (this is PlateKitchenObject)
        {
            plate = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plate = null;
            return false;   
        }
    }
}
