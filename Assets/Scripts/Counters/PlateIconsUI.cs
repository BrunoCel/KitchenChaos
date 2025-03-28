using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    
    void Start()
    {
        iconTemplate.gameObject.SetActive(false);
        plateKitchenObject.OnIngridientAdded += UpdateVisual;
    }

    //private void PlateKitchenObjectOnOnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    //{
       // UpdateVisual();
    //}

    private void UpdateVisual(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetKitchenObjectsList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenIcon(kitchenObjectSo);
        }
    }
}
